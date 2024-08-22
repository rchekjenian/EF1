namespace BNA.EF1.Domain.Common.Interfaces
{
    public interface IAuditableEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}
