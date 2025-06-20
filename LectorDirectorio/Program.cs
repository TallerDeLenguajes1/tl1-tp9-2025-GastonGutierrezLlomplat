Console.WriteLine("-- LECTOR DE DIRECTORIO --");

// 1. Ingresar la ruta
Console.Write("Ingrese el path del directorio que desea analizar: ");
string directorio = Console.ReadLine();

// 2. Verificar si la ruta exite
while (!Directory.Exists(directorio))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("¡El directorio no existe! Ingrese uno nuevamente: ");
    Console.ForegroundColor = ConsoleColor.White;
    directorio = Console.ReadLine();
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("\nDirectorio encontrado correctamente.\n");

// 3. Listar las carpetas
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("El directorio ingresado contiene estas carpetas:\n");

string[] carpetas = Directory.GetDirectories(directorio);
Console.ForegroundColor = ConsoleColor.White;
foreach (var carpeta in carpetas)
{
    Console.WriteLine(carpeta);
}

// 4. Listar los archivos
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("\nEl directorio ingresado contiene estos archivos:\n");

string[] archivos = Directory.GetFiles(directorio);
Console.ForegroundColor = ConsoleColor.White;
foreach (var archivo in archivos)
{
    Console.WriteLine(archivo);
}

// 5. Crear el csv
try
{
    string csvRuta = Path.Combine(directorio, "reporte_archivos.csv");

    using (StreamWriter writer = new StreamWriter(csvRuta))
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
    Console.WriteLine($"\nArchivo CSV creado exitosamente en: {csvRuta}");
}
catch (Exception e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nEl Archivo CSV no fue creado con éxito.");
    Console.WriteLine("Error: " + e.Message);
}

// 6. Restaurar color
Console.ForegroundColor = ConsoleColor.Gray;
