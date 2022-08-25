using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class SessionCart : Cart {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static Cart GetCart(IServiceProvider services) {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }
        public override void AddItem(Product product, int quantity) {
            base.AddItem(product, quantity);
            Session.SetJson("cart", this);
        }
        public override void RemoveLine(Product product) {
            base.RemoveLine(product);
            Session.SetJson("cart", this);
        }
        public override void Clear() {
            base.Clear();
            Session.Remove("cart");
        }
        public override void SetQuantity(long productId, int quantity) {
            base.SetQuantity(productId, quantity);
            Session.SetJson("cart", this);
        }
    }
}
