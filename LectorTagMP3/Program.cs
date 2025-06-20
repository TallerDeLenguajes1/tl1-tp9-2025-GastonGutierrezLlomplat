using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese la ruta del archivo MP3: ");
        string ruta = Console.ReadLine();

        while (!File.Exists(ruta))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("¡La ruta no existe! Ingrese uno nuevamente: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            ruta = Console.ReadLine();
        }

        using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
        {
            if (fs.Length < 128)
            {
                Console.WriteLine("El archivo no contiene TAG");
                return;
            }

            fs.Seek(-128, SeekOrigin.End);

            BinaryReader reader = new BinaryReader(fs, Encoding.GetEncoding("latin1"));

            byte[] buffer = reader.ReadBytes(128);

            string header = Encoding.GetEncoding("latin1").GetString(buffer, 0, 3);
            if (header != "TAG")
            {
                Console.WriteLine("Este archivo no contiene un tag ID3v1 válido.");
                return;
            }

            Id3v1Tag tagMP3 = new Id3v1Tag
            {
                Titulo = Encoding.GetEncoding("latin1").GetString(buffer, 3, 30).TrimEnd('\0', ' '),
                Artista = Encoding.GetEncoding("latin1").GetString(buffer, 33, 30).TrimEnd('\0', ' '),
                Album = Encoding.GetEncoding("latin1").GetString(buffer, 63, 30).TrimEnd('\0', ' '),
                Año = Encoding.GetEncoding("latin1").GetString(buffer, 93, 4).TrimEnd('\0', ' '),
            };

            Console.ForegroundColor = ConsoleColor.Green;
            tagMP3.Mostrar();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}

public class Id3v1Tag
{
    public string Titulo { get; set; }
    public string Artista { get; set; }
    public string Album { get; set; }
    public string Año { get; set; }

    public void Mostrar()
    {
        Console.WriteLine($"Título: {Titulo}");
        Console.WriteLine($"Artista: {Artista}");
        Console.WriteLine($"Álbum: {Album}");
        Console.WriteLine($"Año: {Año}");
    }
}
