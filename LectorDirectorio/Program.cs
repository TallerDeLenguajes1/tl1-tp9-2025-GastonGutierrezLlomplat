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

// Creación del csv
File.Create($"{directorio}\\reporte.csv");

