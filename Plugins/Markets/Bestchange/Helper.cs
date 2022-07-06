using HtmlAgilityPack;

namespace Bestchange
{
    static class Helper
    {
        static void Main()
        {
            string html = File.ReadAllText(@"html.txt");

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var optgroup = doc.DocumentNode.SelectNodes("//optgroup");
            foreach (var group in optgroup)
            {
                var GroupName = group.Attributes["label"].Value;
                Console.WriteLine($"// {GroupName}");
                var items = group.ChildNodes.Where(x => x.Name == "option");
                foreach (var item in items)
                {
                    string Name = item.InnerText.Replace(" ", "").Replace("/", "").Replace("\\", "").Replace("-", "").Replace("(", "_").Replace(")", "");
                    Name = Tr(Name);
                    string Value = item.Attributes["value"].Value;

                    Console.WriteLine($"public static string {Name} => \"{Value}\";");
                }
            }
        }

        private static string Tr(string s)
        {
            string ret = "";
            string[] rus = {"А","Б","В","Г","Д","Е","Ё","Ж", "З","И","Й","К","Л","М", "Н",
                "О","П","Р","С","Т","У","Ф","Х", "Ц", "Ч", "Ш", "Щ",   "Ъ", "Ы","Ь",
                "Э","Ю", "Я",
                "а","б","в","г","д","е","ё","ж", "з","и","й","к","л","м", "н",
                "о","п","р","с","т","у","ф","х", "ц", "ч", "ш", "щ",   "ъ", "ы","ь",
                "э","ю", "я"
            };
            string[] eng = {"A","B","V","G","D","E","E","ZH","Z","I","Y","K","L","M","N",
                "O","P","R","S","T","U","F","KH","TS","CH","SH","SHCH",null,"Y",null,
                "E","YU","YA",
                "a","b","v","g","d","e","e","zh","z","i","y","k","l","m","n",
                "o","p","r","s","t","u","f","kh","ts","ch","sh","sgch",null,"y",null,
                "e","yu","ya"
            };

            for (int j = 0; j < s.Length; j++)
            {
                var Char = s.Substring(j, 1);

                if (rus.Contains(Char))
                {
                    for (int i = 0; i < rus.Length; i++)
                        if (Char == rus[i]) ret += eng[i];
                }
                else
                    ret += Char;
            }

            return ret;
        }
    }
}
