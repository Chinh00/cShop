using Nest;

namespace Core.IndexModels;

public class ElasticEntity<TEntityKey> : IElasticEntity<TEntityKey>
{
    public TEntityKey Id { get; set; }
    public virtual CompletionField Suggest { get; set; }
    public JoinField JoinField { get; set; }
    public virtual string SearchingArea { get; set; }
    public virtual double? Score { get; set; }
}