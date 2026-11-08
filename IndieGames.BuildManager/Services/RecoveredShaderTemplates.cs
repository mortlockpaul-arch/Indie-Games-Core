namespace IndieGames.BuildManager.Services;

// Recovered using HlslDecompiler db004b9094e9d7ef1564ad1815e738d2273b2325.
// Only exact original XNB fingerprints are supported. See Assets/KnownShaders/README.md.
internal static class RecoveredShaderTemplates
{
    internal sealed record Template(string AssetName, string ResourceName);
    internal static readonly Dictionary<string, Template> All = new(StringComparer.OrdinalIgnoreCase)
    {
        ["09D2833C6E513485393C9344940DB66EBCD6F25D2E9C7106D30B5B8F3386E9F4|InstancedModel"] = new("InstancedModel", "PixelArt.InstancedModel"),
        ["6F91484E2692DE16431FDD73EA6539C69F7BAA15FA476589F0A83E4B272B59D9|armShader"] = new("armShader", "DeadKings.armShader"),
        ["3EF1795385E9E66AFE155DFBDE095B5659F206ED9E50D21BB6D2E51B54E51059|blurShader"] = new("blurShader", "DeadKings.blurShader"),
        ["57A6FE2D0963A3DA13B44FA6A4DCAE024A0AAEC68570AB2C20811F0CB1B60DCF|crackShader"] = new("crackShader", "DeadKings.crackShader"),
        ["FFF9484D13C5B070FE04727D94FC51D5D64CB8FB132B57062D152025EC436BA3|crazyShader"] = new("crazyShader", "DeadKings.crazyShader"),
        ["D4BDD6FFF0B8517A3E5A8611C80147EBCC9BBBEC89B6967480B4BCAC0B2653D8|Effect1"] = new("Effect1", "DeadKings.Effect1"),
        ["0A42736ACAB8CB7A92EA844B12CCBDAAFA13FA2E10DFD6384EC46231B495037A|FlatModel"] = new("FlatModel", "DeadKings.FlatModel"),
        ["2001B901527FA443DD8AB7418960E988E2D3F692A2C8C09332D1CF55DCF8E7A3|fogShader"] = new("fogShader", "DeadKings.fogShader"),
        ["0A7B09BDDC9EA27C362C432A78C345C0FBF7D262CC929951B0CA25C38CE274E4|fogShader2"] = new("fogShader2", "DeadKings.fogShader2"),
        ["F3293A4F9B9DEBAA3CA76BFBDE003A49AD18CA4854E6358E173715E8C4381A4A|fogShader2a"] = new("fogShader2a", "DeadKings.fogShader2a"),
        ["E24016D83345AD809B224B3740BA7E4777CE27CF5D922BBA0CABE46F1ED0A5B8|fogShader3"] = new("fogShader3", "DeadKings.fogShader3"),
        ["BBB5C2A210CD15F5E25BE17F0F713FA58010B2A7A2DDF6249EDA0FCE7ECA6889|fogShader3Reflect"] = new("fogShader3Reflect", "DeadKings.fogShader3Reflect"),
        ["60C18C5D08B310FE369666A40DA25A3EE6B24E46E30E43FD1C24A012F06A0794|ghostShader"] = new("ghostShader", "DeadKings.ghostShader"),
        ["4C1F79A86E65C4FF426B106ED29CEC391441B6C35C81AB6BCB1CE0F533CC8167|InstancerDeep"] = new("InstancerDeep", "DeadKings.InstancerDeep"),
        ["6069716AE21E8D67DA53DFD9BE37CE82031C77ABADE8526FA9C41C09457E9321|InstancerSplat"] = new("InstancerSplat", "DeadKings.InstancerSplat"),
        ["4593E2CF908B7C5953D71202FBA2B09BAAF66C22DB11BAAD333FBF6CFCE468FA|InstancerStain"] = new("InstancerStain", "DeadKings.InstancerStain"),
        ["2757489BAF6B263A06E61C152232837BD773B9EED8527856FEAA0A7A8FF5FA7A|InstEffect"] = new("InstEffect", "DeadKings.InstEffect"),
        ["BF03BCB091D92CDCE909FE04D7171FB60B2C0FF8E2C905FF04811A578982C7CE|mayaShader"] = new("mayaShader", "DeadKings.mayaShader"),
        ["A1CA5ECB5082A985E3DED888EABA8DB03E9671B0EFA58D1CFF223150D4F2B35B|multShader"] = new("multShader", "DeadKings.multShader"),
        ["E609EE8D65E3B5CD577D62826298B616CAC4FC739FFBDAD3B99C519628063434|myEffect"] = new("myEffect", "DeadKings.myEffect"),
        ["BD8375FBE8021A502D4C63AFF0D60EB3572E5349230435C491CD4910C91DD76D|ParticleEffect"] = new("ParticleEffect", "DeadKings.ParticleEffect"),
        ["54358B0E625E5F44217E3F0EEF358F229091B5AFED1623B7082646E6D938133A|reflect"] = new("reflect", "DeadKings.reflect"),
        ["4CA40B1C9F92390F7FD557561CB5F85BA4A02A47B4CCC65C5B9CA5B43EAA1563|shadShader"] = new("shadShader", "DeadKings.shadShader"),
        ["280F33A0F38CDAE14440DEE7FA261AC388BE2193D820363A524FA8CBF49EEA29|SimpleInstance"] = new("SimpleInstance", "DeadKings.SimpleInstance"),
        ["CE9FAC04BCE4B43E33E781CA0DA9ED661BE4D78472327711C36A399C37147494|texShader"] = new("texShader", "DeadKings.texShader"),
        ["D6684192865CAC25D694F56422BBD96CEBD4533E81929D230EBC666BD2F3F8E5|waver"] = new("waver", "DeadKings.waver"),
        ["7127A7D850879CC662F2DDFEE6B09E94018B998A721450C9549671869E677C84|AdvancedRadialBlur"] = new("AdvancedRadialBlur", "AdamantAnts.AdvancedRadialBlur"),
        ["CA130C142E789B7499F0E3684828AC34AA9632138EA9B583E5991070D2364775|ClearReflection"] = new("ClearReflection", "AdamantAnts.ClearReflection"),
        ["4D5D121C6D82C4334C19F0FC55C8CA4262E175ECE0A6D8F9AE0E990C50A6E66B|MurkyWater"] = new("MurkyWater", "AdamantAnts.MurkyWater"),
        ["AAE571399FFDF0CDFE1166732DEA04648B1F87CC4CD528A32BDF83123C8F5901|PostProcess"] = new("PostProcess", "AdamantAnts.PostProcess"),
        ["4B1455FB12200128B4636AFC0428C96634C6634EE0450601B18611C7D33D8963|PureReflection"] = new("PureReflection", "AdamantAnts.PureReflection"),
        ["1F015017F3E83ECBC896FAE8512CE502CF2A3CF1EF627589EF4B3D560AEF84F4|RedWaterReflection"] = new("RedWaterReflection", "AdamantAnts.RedWaterReflection"),
        ["9B553424B243130B19B6736E53A7CB0C3B2B7A0AFF1BDF210F35D80A2815A32A|WaterReflection"] = new("WaterReflection", "AdamantAnts.WaterReflection"),
        ["7CA9478A8B99EF0E5BBDBEAA3A69A4638A75E309548721C80A33D3AA66DC51EF|WaterReflection2"] = new("WaterReflection2", "AdamantAnts.WaterReflection2"),
        ["4B1455FB12200128B4636AFC0428C96634C6634EE0450601B18611C7D33D8963|WaterReflection3"] = new("WaterReflection3", "AdamantAnts.WaterReflection3"),
        ["8E8A4E5D918C7AC67D6408AD279A5AFAF7436CFFC79F10FA219EF1FB2B546146|WaterReflection6"] = new("WaterReflection6", "AdamantAnts.WaterReflection6"),
        ["BB19CC849DA07BD2A5E7BCCDF00E1C1F33A18E6B0F558489B0C14B1FD21B0007|WaterReflection7"] = new("WaterReflection7", "AdamantAnts.WaterReflection7"),
    };
}
