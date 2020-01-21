One more implementation of Dependency Injection pattern 
Supports Transient and Singleton scopes. 
Supports scopes

 ContainerBuilder cb = new ContainerBuilder();
            cb.Register<FirstService>();

            using (var c1 = cb.Build())
            {
                var fs1 = c1.Resolve<FirstService>();

                using (var c2 = c1.NewScope())
                {
                    var fs2 = c2.Resolve<FirstService>();

                    Assert.NotEqual(fs1, fs2);
                }
            }
