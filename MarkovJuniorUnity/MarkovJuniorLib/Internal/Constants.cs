using System;

namespace MarkovJuniorLib.Internal
{
    internal static class Constants
    {
        public const string Font_Tamzen8x16b_BASE64 = "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";
        public const string Font_Tamzen8x16r_BASE64 = "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";

        private static Lazy<byte[]> _font_Tamzen8x16b_Bytes = new Lazy<byte[]>(() => Convert.FromBase64String(Font_Tamzen8x16b_BASE64));
        private static Lazy<byte[]> _font_Tamzen8x16r_Bytes = new Lazy<byte[]>(() => Convert.FromBase64String(Font_Tamzen8x16r_BASE64));
        public static byte[] Font_Tamzen8x16b_Bytes => _font_Tamzen8x16b_Bytes.Value;
        public static byte[] Font_Tamzen8x16r_Bytes => _font_Tamzen8x16r_Bytes.Value;

        public const string Settings_XML = @"
<settings
  squareSize=""7"" smallSquareSize=""3"" maxwidth=""10"" zshift=""2""
  hindent=""30"" hgap=""2"" harrow=""10"" hline=""14""
  vskip=""2"" smallvskip=""2"" fontshift=""2"" afterfont=""4""
  dense=""True"" d3=""True""
  background=""222222"" inactive=""666666"" active=""ffffff""
/>";

        public const string Pallette_XML = @"
<colors>
  <color symbol=""B"" value=""000000"" comment=""Black""/>
  <color symbol=""I"" value=""1D2B53"" comment=""Indigo""/>
  <color symbol=""P"" value=""7E2553"" comment=""Purple""/>
  <color symbol=""E"" value=""008751"" comment=""Emerald""/>
  <color symbol=""N"" value=""AB5236"" comment=""browN""/>
  <color symbol=""D"" value=""5F574F"" comment=""Dead, Dark""/>
  <color symbol=""A"" value=""C2C3C7"" comment=""Alive, grAy""/>
  <color symbol=""W"" value=""FFF1E8"" comment=""White""/>
  <color symbol=""R"" value=""FF004D"" comment=""Red""/>
  <color symbol=""O"" value=""FFA300"" comment=""Orange""/>
  <color symbol=""Y"" value=""FFEC27"" comment=""Yellow""/>
  <color symbol=""G"" value=""00E436"" comment=""Green""/>
  <color symbol=""U"" value=""29ADFF"" comment=""blUe""/>
  <color symbol=""S"" value=""83769C"" comment=""Slate""/>
  <color symbol=""K"" value=""FF77A8"" comment=""pinK""/>
  <color symbol=""F"" value=""FFCCAA"" comment=""Fawn""/>
  
  <color symbol=""b"" value=""291814"" comment=""black""/>
  <color symbol=""i"" value=""111d35"" comment=""indigo""/>
  <color symbol=""p"" value=""422136"" comment=""purple""/>
  <color symbol=""e"" value=""125359"" comment=""emerald""/>
  <color symbol=""n"" value=""742f29"" comment=""brown""/>
  <color symbol=""d"" value=""49333b"" comment=""dead, dark""/>
  <color symbol=""a"" value=""a28879"" comment=""alive, gray""/>
  <color symbol=""w"" value=""f3ef7d"" comment=""white""/>
  <color symbol=""r"" value=""be1250"" comment=""red""/>
  <color symbol=""o"" value=""ff6c24"" comment=""orange""/>
  <color symbol=""y"" value=""a8e72e"" comment=""yellow""/>
  <color symbol=""g"" value=""00b543"" comment=""green""/>
  <color symbol=""u"" value=""065ab5"" comment=""blue""/>
  <color symbol=""s"" value=""754665"" comment=""slate""/>
  <color symbol=""k"" value=""ff6e59"" comment=""pink""/>
  <color symbol=""f"" value=""ff9d81"" comment=""fawn""/>
  
  <color symbol=""C"" value=""00ffff"" comment=""Cyan""/>
  <color symbol=""c"" value=""5fcde4"" comment=""cyan""/>
  <color symbol=""H"" value=""e4bb40"" comment=""Honey""/>
  <color symbol=""h"" value=""8a6f30"" comment=""honey""/>
  <color symbol=""J"" value=""4b692f"" comment=""Jungle""/>
  <color symbol=""j"" value=""45107e"" comment=""jungle""/>
  <color symbol=""L"" value=""847e87"" comment=""Light""/>
  <color symbol=""l"" value=""696a6a"" comment=""light""/>
  <color symbol=""M"" value=""ff00ff"" comment=""Magenta""/>
  <color symbol=""m"" value=""9c09cc"" comment=""magenta""/>
  <color symbol=""Q"" value=""9badb7"" comment=""aQua""/>
  <color symbol=""q"" value=""3f3f74"" comment=""aqua""/>
  <color symbol=""T"" value=""37946e"" comment=""Teal""/>
  <color symbol=""t"" value=""323c39"" comment=""teal""/>
  <color symbol=""V"" value=""8f974a"" comment=""oliVe""/>
  <color symbol=""v"" value=""524b24"" comment=""olive""/>
  <color symbol=""X"" value=""ff0000"" comment=""X""/>
  <color symbol=""x"" value=""d95763"" comment=""x""/>
  <color symbol=""Z"" value=""ffffff"" comment=""Z""/>
  <color symbol=""z"" value=""cbdbfc"" comment=""z""/>
</colors>";
    }
}
