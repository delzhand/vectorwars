using System;
using System.Text;

[Serializable]
public class BasicResponse
{
    public int api_version;
    public int data_major_version;
    public int data_minor_version;
    public int patch_version;
    public string name;
    public int time;
    public string method;
    public string type;

    public string VersionNumber()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(data_major_version);
        sb.Append(".");
        sb.Append(data_minor_version);
        sb.Append(".");
        sb.Append(patch_version);
        return sb.ToString();
    }
}
