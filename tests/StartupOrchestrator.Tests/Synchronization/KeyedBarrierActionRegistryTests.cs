using StartupOrchestrator.Synchronization;

namespace StartupOrchestrator.Tests.Synchronization
{
    public class KeyedBarrierActionRegistryTests
    {
        [Fact(DisplayName = "Callback works when instances are triggered before registration")]
        public void TriggerBeforeRegister_InvokesCallbackLate()
        {
            var registry = new KeyedBarrierActionRegistry();

            var user = new TestUser();
            var order = new TestOrder();
            var product = new TestProduct();

            registry.Trigger(user);
            registry.Trigger(order);
            registry.Trigger(product);

            TestUser? capturedUser = null;
            TestOrder? capturedOrder = null;
            TestProduct? capturedProduct = null;

            registry
                .Register<TestProduct>()
                .Register<TestOrder>()
                .RegisterCallback<TestUser>(ctx =>
                {
                    capturedUser = ctx.Get<TestUser>();
                    capturedOrder = ctx.Get<TestOrder>();
                    capturedProduct = ctx.Get<TestProduct>();
                });

            Assert.Same(user, capturedUser);
            Assert.Same(order, capturedOrder);
            Assert.Same(product, capturedProduct);
        }

        [Fact(DisplayName = "Callback receives keyed and unkeyed instances together")]
        public void TriggerKeyedAndUnkeyed_InvokesCallback()
        {
            var registry = new KeyedBarrierActionRegistry();

            TestUser? capturedUser = null;
            TestOrder? capturedOrder = null;
            TestProduct? capturedProduct = null;

            registry
                .Register<TestProduct>()
                .Register<TestOrder>("orderSpecial")
                .RegisterCallback<TestUser>(ctx =>
                {
                    capturedUser = ctx.Get<TestUser>();
                    capturedOrder = ctx.Get<TestOrder>("orderSpecial");
                    capturedProduct = ctx.Get<TestProduct>();
                });

            var user = new TestUser();
            var order1 = new TestOrder { Name = "1" };
            var order2 = new TestOrder { Name = "2" };
            var product = new TestProduct();

            registry.Trigger(user);
            registry.Trigger(order1);
            registry.Trigger(product);
            registry.Trigger(order2, "orderSpecial");

            Assert.Same(user, capturedUser);
            Assert.Same(order2, capturedOrder);
            Assert.Same(product, capturedProduct);
        }

        [Fact(DisplayName = "Callback receives keyed instance correctly")]
        public void TriggerKeyedInstance_InvokesCallback()
        {
            var registry = new KeyedBarrierActionRegistry();

            TestUser? capturedUser = null;

            registry.RegisterCallback<TestUser>(ctx =>
            {
                capturedUser = ctx.Get<TestUser>("userSpecial");
            }, "userSpecial");

            registry.Trigger(new TestUser { Id = 1 });
            registry.Trigger(new TestUser { Id = 3 }, "userSpecial");

            Assert.NotNull(capturedUser);
            Assert.Equal(3, capturedUser!.Id);
        }

        [Fact(DisplayName = "Callback receives multiple types correctly")]
        public void TriggerMultipleInstances_InvokesCallbackWithAllTypes()
        {
            var registry = new KeyedBarrierActionRegistry();

            TestUser? capturedUser = null;
            TestOrder? capturedOrder = null;

            registry
                .Register<TestOrder>()
                .RegisterCallback<TestUser>(ctx =>
                {
                    capturedUser = ctx.Get<TestUser>();
                    capturedOrder = ctx.Get<TestOrder>();
                });

            var user = new TestUser();
            var order = new TestOrder();

            registry.Trigger(user);
            registry.Trigger(order);

            Assert.Same(user, capturedUser);
            Assert.Same(order, capturedOrder);
        }

        [Fact(DisplayName = "Callback receives specific keyed and first unkeyed instance")]
        public void TriggerMultipleKeyedInstances_InvokesCallbackCorrectly()
        {
            var registry = new KeyedBarrierActionRegistry();

            var user1 = new TestUser { Id = 1 };
            var user2 = new TestUser { Id = 2 };
            var user3 = new TestUser { Id = 3 };
            var user4 = new TestUser { Id = 4 };
            var user5 = new TestUser { Id = 5 };

            registry.Trigger(user1, "user1");
            registry.Trigger(user2, "user2");
            registry.Trigger(user3, "user3");
            registry.Trigger(user4, "user4");
            registry.Trigger(user5, "user5");

            TestUser? capturedKeyed = null;
            TestUser? capturedUnkeyed = null;

            registry.RegisterCallback<TestUser>(ctx =>
            {
                capturedKeyed = ctx.Get<TestUser>("user4");
                capturedUnkeyed = ctx.Get<TestUser>();
            }, "user4");

            Assert.Same(capturedKeyed, capturedUnkeyed);
        }

        [Fact(DisplayName = "Callback is invoked when single instance is triggered")]
        public void TriggerSingleInstance_InvokesCallback()
        {
            var registry = new KeyedBarrierActionRegistry();

            TestUser? capturedUser = null;

            registry.RegisterCallback<TestUser>(ctx =>
            {
                capturedUser = ctx.Get<TestUser>();
            });

            var user = new TestUser();
            registry.Trigger(user);

            Assert.Same(user, capturedUser);
        }

        [Fact(DisplayName = "Callback receives three types correctly")]
        public void TriggerThreeInstances_InvokesCallbackWithAllTypes()
        {
            var registry = new KeyedBarrierActionRegistry();

            TestUser? capturedUser = null;
            TestOrder? capturedOrder = null;
            TestProduct? capturedProduct = null;

            registry
                .Register<TestProduct>()
                .Register<TestOrder>()
                .RegisterCallback<TestUser>(ctx =>
                {
                    capturedUser = ctx.Get<TestUser>();
                    capturedOrder = ctx.Get<TestOrder>();
                    capturedProduct = ctx.Get<TestProduct>();
                });

            var user = new TestUser();
            var order = new TestOrder();
            var product = new TestProduct();

            registry.Trigger(user);
            registry.Trigger(order);
            registry.Trigger(product);

            Assert.Same(user, capturedUser);
            Assert.Same(order, capturedOrder);
            Assert.Same(product, capturedProduct);
        }
    }

    public class TestOrder
    {
        public string Name { get; set; } = string.Empty;
    }

    public class TestProduct
    {
        public decimal Price { get; set; }
    }

    public class TestUser
    {
        public int Id { get; set; }
    }
}
