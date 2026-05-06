using RobotShop.Models;

namespace RobotShop.Services.Interfaces
{
	public interface IPdfService
	{
		byte[] GenerateProductSpecificationPdf(string productId);
	}
}
