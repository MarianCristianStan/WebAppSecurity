namespace RobotShop.Repositories.Interfaces
{
   public interface IRepositoryWrapper
   {
      IUserRepository UserRepository { get; }
      IUserAddressRepository UserAddressRepository { get; }
      IProductRepository ProductRepository { get; }
      IProductCategoryRepository ProductCategoryRepository { get; }
      IFeatureRepository FeatureRepository { get; }
      IProductFeatureRepository ProductFeatureRepository { get; }
      IOrderRepository OrderRepository { get; }
      ICartRepository CartRepository { get; }
      ICartProductRepository CartProductRepository { get; }
      IOrderProductRepository OrderProductRepository { get; }
     
      void Save();
   }
}
