namespace Nubot.Plugins.Samples.Stash.Models
{
    public class Value1
    {
        public string ContentId { get; set; }
        public Path Path { get; set; }
        public bool Executable { get; set; }
        public int PercentUnchanged { get; set; }
        public string Type { get; set; }
        public string NodeType { get; set; }
        public bool SrcExecutable { get; set; }
        public Link Link { get; set; }
    }
}