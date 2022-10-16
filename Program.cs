using Impexpdata;

args[0] = "-import";
args[1] = "C:\\Users\\syuneg\\customers.csv";

//C:\Users\syuneg\customers.csv
var csvProcessor = new CsvProcessor();

if (args.Length == 2)
{

    switch (args[0])
    {
        case "-import":
            string filePath = args[1];
            if (File.Exists(filePath))
            {
                csvProcessor.ImportDataFromCsv(filePath);
            }
            else
                Console.WriteLine("Path not found");
            break;
        case "-export":
            filePath = args[1];
            csvProcessor.ExportDataToCsv(filePath);
            break;
        default:
            Console.WriteLine("Please, specify operation (-export fileName, -import fileName)");
            break;
    }
}
else
{
    Console.WriteLine("Please, specify operation (-export fileName, -import fileName)");
}

