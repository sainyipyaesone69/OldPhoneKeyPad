using System.Threading.Channels;
using System.Text;
using System.Runtime.CompilerServices;

class Program
{
    private static readonly Dictionary<char, string> KeyPad=new() { {'2',"ABC"}, { '3', "DEF" }, { '4', "GHI" }, { '5', "JKL" }, { '6', "MNO" }, { '7', "PQRS" }, { '8', "TUV" }, { '9', "WXYZ" }, { '0', " " } };

    public static void Main()
    {
        //Console.Write("Please enter digits to convert :");
        //string input= Console.ReadLine();
        //if (input[input.Length - 1] != '#')
        //{
        //    input += "#";
        //}
        //Console.WriteLine(OldPhonePad(input));
        Console.WriteLine(OldPhonePad("33#"));
        Console.WriteLine(OldPhonePad("227*#"));
        Console.WriteLine(OldPhonePad("4433555 555666#"));
        Console.WriteLine(OldPhonePad("8 88777444666*664#"));
    }

    private static string OldPhonePad(string str)
    {
        char? lastChar = null;
        StringBuilder result = new StringBuilder();
        StringBuilder currentLetter = new StringBuilder();
        foreach (char c in str)
        {
            if (char.IsDigit(c) && KeyPad.ContainsKey(c))
            {
                if (c == lastChar)
                {
                    currentLetter.Append(c);
                }
                else
                {
                    GetLetter(currentLetter, result);
                    currentLetter.Append(c);
                }
            }
            else if (c == '*')
            {
                if (currentLetter.Length > 0)
                {
                    currentLetter.Clear();
                }

                else if (result.Length > 0)
                {
                    result.Length--;
                }
            }
            else if (c == ' ')
            {
                GetLetter(currentLetter,result);
            }
            else if (c == '#')
            {
                GetLetter(currentLetter, result);
                break;
            }
            lastChar = c;
        }
        return result.ToString();
    }
    private static void GetLetter(StringBuilder currentletter,StringBuilder result)
    {
        if(currentletter.Length==0) return;
        char key = currentletter[0];
        int digitCount = currentletter.Length;
        if(KeyPad.TryGetValue(key, out string? letters))
        {
            result.Append(letters[(digitCount-1) % letters.Length]);
        } 
        currentletter.Clear();
    }

}
