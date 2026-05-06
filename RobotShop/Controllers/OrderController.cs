using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RobotShop.Models;
using RobotShop.Services;
using RobotShop.Services.Interfaces;

namespace Mister_Robot.Controllers
{
   [Authorize]
   public class OrderController : Controller
   {
      private readonly IOrderService _orderService;
      private readonly IUserService _userService;
      private readonly IProductService _productService;

      public OrderController(IOrderService orderService, IUserService userService, IProductService productService)
      {
         _orderService = orderService;
         _userService = userService;
         _productService = productService;
      }

     
      public IActionResult Index()
      {
         var user = _userService.GetCurrentUser();
         var orders = _orderService.GetOrdersByUserId(user.Id);
         return View(orders);  
      }


      public IActionResult OrderDetails(string orderId)
      {
         var user = _userService.GetCurrentUser();
         var order = _orderService.GetOrderById(orderId);

         if (order == null)
         {
            return NotFound("Order not found.");
         }

         if (order.UserId != user.Id)
         {
            return Forbid("You do not have permission to view this order.");
         }

         return View(order);
      }

      public IActionResult SoldProducts()
      {
         var soldProducts = _orderService.GetSoldProducts();
         return View(soldProducts);
      }


	}
}