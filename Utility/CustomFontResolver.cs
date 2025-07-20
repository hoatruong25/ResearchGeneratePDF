using System.Reflection;
using PdfSharpCore.Fonts;

public class CustomFontResolver : IFontResolver
{
    public string DefaultFontName => "Arial";
    
    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        string name = familyName.ToLower().TrimEnd('#');
        string faceName = name;

        switch (name)
        {
            case "arial":
                if (isBold && isItalic)
                    faceName = "Arial#bi";
                else if (isBold)
                    faceName = "Arial#b";
                else if (isItalic)
                    faceName = "Arial#i";
                else
                    faceName = "Arial#";
                break;

            case "times new roman":
                if (isBold && isItalic)
                    faceName = "TimesNewRoman#bi";
                else if (isBold)
                    faceName = "TimesNewRoman#b";
                else if (isItalic)
                    faceName = "TimesNewRoman#i";
                else
                    faceName = "TimesNewRoman#";
                break;

            default:
                return PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);
        }

        return new FontResolverInfo(faceName, isItalic, isBold);
    }

    public byte[]? GetFont(string faceName)
    {
        switch (faceName)
        {
            case "Arial#":
                return LoadFontData("TestPDFSharp.Fonts.Arial.TTF");
            case "Arial#b":
                return LoadFontData("TestPDFSharp.Fonts.Arialbd.TTF");
            case "Arial#i":
                return LoadFontData("TestPDFSharp.Fonts.Ariali.TTF");
            case "Arial#bi":
                return LoadFontData("TestPDFSharp.Fonts.Arialbi.TTF");
            case "TimesNewRoman#":
                return LoadFontData("TestPDFSharp.Fonts.Times.TTF");
            case "TimesNewRoman#b":
                return LoadFontData("TestPDFSharp.Fonts.Timesbd.TTF");
            case "TimesNewRoman#i":
                return LoadFontData("TestPDFSharp.Fonts.Timesi.TTF");
            case "TimesNewRoman#bi":
                return LoadFontData("TestPDFSharp.Fonts.Timesbi.TTF");
            default:
                return null;
        }
    }


    private byte[] LoadFontData(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
                throw new ArgumentException($"Không tìm thấy phông chữ: {resourceName}");
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            return data;
        }
    }

    public static void Apply()
    {
        GlobalFontSettings.FontResolver = new CustomFontResolver();
    }
}