using cShop.Core.Specifications;
using Domain;

namespace Application.UseCases.Specs;

public sealed class GetOrderInfoByTxnRefSpec : SpecificationBase<OrderInfo>
{
    public GetOrderInfoByTxnRefSpec(int  txnRef)
    {
        ApplyFilter(e => e.TxnRef == txnRef && e.Status == PaymentStatus.Pending);
    }
}