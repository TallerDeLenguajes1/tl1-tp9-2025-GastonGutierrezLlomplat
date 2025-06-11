Console.WriteLine("-- LECTOR DE DIRECTORIO --");
Console.Write("Ingrese el path del directorio que desea analizar: ");

string directorio = Console.ReadLine();

while (!Directory.Exists(directorio))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("¡El directorio no existe! Ingrese uno nuevamente: ");
    Console.ForegroundColor = ConsoleColor.White;
    directorio = Console.ReadLine();
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Directorio encontrado correctamente.");
Console.WriteLine("");
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("El directorio ingresado contiene estas carpetas: ");
Console.WriteLine("");

string[] carpetas = Directory.GetDirectories(directorio);


Console.ForegroundColor = ConsoleColor.White;
foreach (var carpeta in carpetas)
{
    Console.WriteLine(carpeta);
}

Console.WriteLine("");
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("El directorio ingresado contiene estos archivos: ");
Console.WriteLine("");

string[] archivos = Directory.GetFiles(directorio);

Console.ForegroundColor = ConsoleColor.White;
foreach (var archivo in archivos)
{
    Console.WriteLine(archivo);
}

try
{
    // Creación del csv
    File.Create($"{directorio}\\reporte.csv");
    // Path del csv
    string csvPath = Path.Combine(directorio, "reporte_archivos.csv");
    using (StreamWriter writer = new StreamWriter(csvPath))
    {
        writer.WriteLine("Nombre del Archivo;Tamaño (KB);Fecha de Última Modificación");
        foreach (var archivo in archivos)
        {
            FileInfo info = new FileInfo(archivo);
            string nombre = info.Name;
            double tamañoKB = Math.Round((double)info.Length / 1024, 2);
            string fechaMod = info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            writer.WriteLine($"{nombre};{tamañoKB};{fechaMod}");
        }
    }
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("");
    Console.WriteLine($"Archivo CSV creado exitosamente en: {csvPath}");
    Console.ForegroundColor = ConsoleColor.Gray;
}
catch (Exception)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("El Archivo CSV no fue creado con éxito.");
}