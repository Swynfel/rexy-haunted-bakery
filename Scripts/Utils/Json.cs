using Godot;
using Godot.Collections;

namespace Utils {
    public static class Json {
        public static Dictionary DictFromFile(string path) {
            File file = new File();
            Error success = file.Open(path, File.ModeFlags.Read);
            if (success != Error.Ok) {
                GD.PrintErr($"Failed to open {path}");
                return null;
            }
            string stringData = file.GetAsText();
            file.Close();
            JSONParseResult result = JSON.Parse(stringData);
            if (result.Error != Error.Ok) {
                GD.PrintErr($"Failed to parse json {path}:\n{result.ErrorString}");
            }
            return (Dictionary) result.Result;
        }
    }
}