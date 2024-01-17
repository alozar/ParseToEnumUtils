using System.Text;

namespace ParseToEnumUtils;

public class ParseToEnumService
{
    private readonly ReadFilesService _readFilesService;

    public ParseToEnumService(ReadFilesService readFilesService)
    {
        _readFilesService = readFilesService;
    }

    public async Task ParseAndCreateAsync(string pathSourse, string creatingFileName)
    {
        var list = await _readFilesService.ReadAsync(pathSourse);
        var result = new StringBuilder();

        for (var i = 0; i < list.Count; i++)
         {
            var splitTabArray = list[i].Split('\t');
            var id = Convert.ToInt32(splitTabArray[0].Trim());

            Console.WriteLine(id);
            // Обработка имени/описания
            var wordInd = Math.Max(splitTabArray[1].IndexOf(" "), Math.Max(splitTabArray[1].IndexOf("-"), splitTabArray[1].IndexOf("–")));
            var tableName = splitTabArray[1];
            if (wordInd > 0)
            {
                tableName = tableName.Substring(0, wordInd).Trim();
            }

            // Обрезаем имя
            var tableNameParts = tableName.Split('.').Last().ToLower().Split('_');
            // Переводим в CamelCase
            tableName = string.Join("", tableNameParts.Select(part => char.ToUpper(part[0]) + part.Substring(1)));

            result.AppendLine("/// <summary>");
            result.AppendLine($"/// {splitTabArray[1]}");
            result.AppendLine("/// </summary>");
            result.Append($"{tableName} = {id}");
            result.AppendLine($"{(i == list.Count - 1 ? "" : ",")}");
            result.AppendLine();
        }

        var itog = result.ToString();
    }
}
