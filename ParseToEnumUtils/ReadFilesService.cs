using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseToEnumUtils;

public class ReadFilesService
{
    public async Task<List<string>> ReadAsync(string path)
    {
        if (!File.Exists(Path.GetFullPath(path)))
        {
            throw new FileNotFoundException(path + " файл не существует!");
        }
        using (StreamReader reader = new StreamReader(path))
        {
            var list = new List<string>();
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                list.Add(line);
            }
            return list;
        }
    }
}
