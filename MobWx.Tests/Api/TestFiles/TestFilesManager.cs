namespace MobWx.Tests.Api.TestFiles;

public class TestFilesManager
{
    private readonly string _testFilesDirectory;

    public TestFilesManager()
    {
        _testFilesDirectory = Path.Combine(AppContext.BaseDirectory, "Api", "TestFiles");
    }

    public string LoadFileContent(string fileName)
    {
        string filePath = Path.Combine(_testFilesDirectory, fileName);

        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }

        throw new FileNotFoundException($"Test file '{fileName}' not found in '{_testFilesDirectory}'.");
    }
}
