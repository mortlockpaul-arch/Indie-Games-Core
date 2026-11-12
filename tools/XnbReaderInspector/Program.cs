using System.Buffers.Binary;
using System.Text;
using XnbExtractor.Compression;

if (args.Length != 1 || !File.Exists(args[0])) { Console.Error.WriteLine("Usage: dotnet run --project XnbReaderInspector.csproj -- <file.xnb>"); return 2; }
try {
    byte[] data = File.ReadAllBytes(args[0]);
    if (data.Length < 10 || Encoding.ASCII.GetString(data,0,3)!="XNB") throw new InvalidDataException("Not an XNB");
    byte flags=data[5]; int declared=BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(6,4));
    Console.WriteLine($"File: {args[0]}\nPlatform: {(char)data[3]} Version: {data[4]} Flags: 0x{flags:X2} Declared file size: {declared} Actual: {data.Length}");
    if ((flags & 0x40)!=0) throw new NotSupportedException("LZ4 compression is not supported by this inspector");
    byte[] payload;
    if ((flags & 0x80)!=0) {
        if(data.Length<14) throw new InvalidDataException("Truncated LZX header");
        int size=BinaryPrimitives.ReadInt32LittleEndian(data.AsSpan(10,4));
        if(size<=0 || size>512*1024*1024) throw new InvalidDataException("Invalid decompressed size");
        using var input=new MemoryStream(data,14,data.Length-14,false);
        using var decoder=new LzxDecoderStream(input,size,data.Length-14);
        using var output=new MemoryStream(); decoder.CopyTo(output); payload=output.ToArray();
        if(payload.Length!=size) throw new InvalidDataException($"Decompressed {payload.Length} bytes, expected {size}");
        Console.WriteLine($"LZX decompressed payload: {payload.Length} bytes");
    } else payload=data[10..];
    using var stream=new MemoryStream(payload); using var reader=new BinaryReader(stream,Encoding.UTF8);
    int count=Read7Bit(reader); if(count<0||count>4096) throw new InvalidDataException("Invalid reader count");
    Console.WriteLine($"\nReader table ({count} entries):");
    var names=new List<string>();
    for(int i=0;i<count;i++) {string name=reader.ReadString(); int version=reader.ReadInt32();names.Add(name);Console.WriteLine($"  [{i+1}] readerVersion={version} {name}");}
    int shared=Read7Bit(reader); int root=Read7Bit(reader);
    Console.WriteLine($"\nShared resource count: {shared}\nRoot reader index: {root}");
    Console.WriteLine(root==0?"Root object is null":root>0&&root<=names.Count?$"Root reader: {names[root-1]}":"Root index outside reader table");
    for(int i=0;i<names.Count;i++) if(names[i].Contains("PipelineInfoReader",StringComparison.OrdinalIgnoreCase))
        Console.WriteLine($"PipelineInfoReader table index: {i+1}; root reader: {root==i+1}");
    Console.WriteLine($"Root payload starts at decompressed offset {stream.Position}; remaining bytes: {stream.Length-stream.Position}");
    long payloadStart = reader.BaseStream.Position;

    int length = (int)Math.Min(
        256,
        reader.BaseStream.Length - payloadStart
    );

    payload = reader.ReadBytes(length);

    Console.WriteLine();
    Console.WriteLine("=== Root payload: first 256 bytes ===");

    Console.WriteLine(
        Convert.ToHexString(payload)
    );

    Console.WriteLine();
    Console.WriteLine("=== ASCII representation ===");

    foreach (byte value in payload)
    {
        Console.Write(
            value >= 32 && value <= 126
                ? (char)value
                : '.'
        );
    }

    Console.WriteLine();
    Console.WriteLine("Note: nested object reader usage cannot be established without deserializing the object graph.");
    return 0;
} catch(Exception e) {Console.Error.WriteLine(e);return 1;}
static int Read7Bit(BinaryReader r) {int value=0; for(int shift=0;shift<35;shift+=7) {int b=r.ReadByte();if(shift==28 && (b&0xF0)!=0) throw new InvalidDataException("7-bit integer overflow");value|=(b&0x7f)<<shift;if((b&0x80)==0)return value;}throw new InvalidDataException("Invalid 7-bit integer");}
