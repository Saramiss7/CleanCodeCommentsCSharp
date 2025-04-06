namespace CleanCodeComments;

class Program
{
    static void Main()
    {
        bool comentarimultiple = false;
        
        Console.WriteLine("Entra el nom del fitxer amb la seva extensió:");
        var filename = Console.ReadLine();
        
        try
        {
            using var reader = new StreamReader(filename);
            var nom = Path.GetFileNameWithoutExtension(filename);
            var extensio = Path.GetExtension(filename);
            using var writer = new StreamWriter($"{nom}-nou{extensio}");
            
            while (!reader.EndOfStream)
            {
                var linia = reader.ReadLine();
                var novaLinia = "";
                for (int i = 0; i < linia.Length; i++)
                {
                    if (!comentarimultiple)
                    {
                        if (i < linia.Length -1 && linia[i] == '/' && linia[i+1] == '/')
                        {
                            break;
                        }
                        if (i < linia.Length - 1 && linia[i] == '/' && linia[i + 1] == '*')
                        {
                            comentarimultiple = true;
                            i++;
                            continue;
                        }
                        novaLinia += linia[i];
                    }
                    else if (i <linia.Length-1 && linia[i] == '*' && linia[i + 1] == '/')
                    {
                        comentarimultiple = false; 
                        i++; 
                    }
                }
                if (!string.IsNullOrWhiteSpace(novaLinia)) //En cas de que quedin espais o linies en blanc, eliminar-los.
                {
                    writer.WriteLine(novaLinia);
                }
            }
            Console.WriteLine("Eliminant espais i comentaris");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("El fitxer no s'ha trobat");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine($"Generat {filename}");
    }
}