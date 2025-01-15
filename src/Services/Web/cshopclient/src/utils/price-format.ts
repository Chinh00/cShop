class PriceFormat {
    static ConvertVND(amount: number): string {
        return new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND',
        }).format(amount)
    }    
}
export default PriceFormat;