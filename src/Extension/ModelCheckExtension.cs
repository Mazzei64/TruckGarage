namespace TruckGarage.Extension;

public static class ModelCheckExtension {
    public static bool IsFM_FH(this string model) {
        int index = 0;
        while(model[index] != ' ' && index + 1 < model.Length) index++;
        if(index + 1 != model.Length - 2) return false;
        string tag = model.Substring(index + 1);
        if(String.Compare(tag, "FM") != 0 && String.Compare(tag, "FH") != 0)
            return false;
        return true;
    }
    public static bool IsYear(this string str){
        if(str.Length != 4 || !str.IsNumeric()) return false;
        return true;
    }
    public static bool IsNumeric(this string num) {
        for(int i = 0; i<num.Length; i++)
            if(num[i] < 0x30 || num[i] > 0x39)
                return false;
        return true;
    }
}