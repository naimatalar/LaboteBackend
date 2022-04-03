using Labote.Core.BindingModels;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core
{
    public interface ILaboteContextDbSeed
    {
        Task<bool> CreateRoleAndUsersAsync();
        Task<bool> CreateDefaultRoleAsync();
        Task<bool> MenuSeedAsync();

    }

    public class LaboteContextDbSeed : ILaboteContextDbSeed
    {
        private readonly UserManager<LaboteUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        public IConfiguration Configuration { get; }

        public LaboteContextDbSeed(UserManager<LaboteUser> userManager, RoleManager<UserRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            Configuration = configuration;
        }



        public async Task<bool> CreateRoleAndUsersAsync()
        {
            try
            {
                var topic = new UserTopic
                {
                    Code = Guid.NewGuid().ToString("N")
                };

                using (LaboteContext context = new LaboteContext())
                {
                    if (_userManager.Users.Count() == 0)
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {

                            context.UserTopics.Add(topic);
                            context.SaveChanges();
                            transaction.Commit();
                        }
                    }

                }
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {

                        LaboteUser user = new LaboteUser();
                        if (_userManager.Users.Count() == 0)
                        {
                            user = new LaboteUser()
                            {
                                Email = "mail@mail.com",
                                SecurityStamp = Guid.NewGuid().ToString(),
                                UserName = "Varsayilan",
                                FirstName = "Varsayılan",
                                Lastname = "Kullanici",
                                EmailConfirmed = true,
                                NotDelete = true,
                                UserTopicId = topic.Id,

                            };
                            var usr = _userManager.CreateAsync(user, "Tg71!nG*").Result;
                        }
                        if (_roleManager.Roles.Count() == 0)
                        {
                            var admninRol = _roleManager.CreateAsync(new UserRole
                            {
                                Name = Enums.Admin.ToString(),
                                NormalizedName = Enums.Admin.ToString().ToUpper(),
                                NotDelete = true

                            }).Result;
                            var userRol = _roleManager.CreateAsync(
                                new UserRole
                                {
                                    Name = Enums.User.ToString(),
                                    NormalizedName = Enums.User.ToString().ToUpper()
                                }
                                ).Result;
                            var RoleAdd = _userManager.AddToRoleAsync(user, Enums.Admin).Result;

                        }

                        transaction.Commit();

                    };

                }

                using (LaboteContext context = new LaboteContext())
                {

                    using (var transaction = context.Database.BeginTransaction())
                    {
                        if (context.UserMenuModules.Count() == 0)
                        {
                            var data = context.MenuModules.ToList();
                            var role = context.UserRoles.FirstOrDefault();

                            foreach (var item in data)
                            {
                                context.UserMenuModules.Add(new UserMenuModule
                                {
                                    UserRoleId = role.RoleId,
                                    MenuModelId = item.Id
                                });
                            }
                            context.SaveChanges();
                            transaction.Commit();
                        }
                    };
                }
            }
            catch (Exception e)
            {

                throw;
            }



            return true;
        }
        public async Task<bool> MenuSeedAsync()
        {
            var MenuList = new List<MenuModule>();


            using (LaboteContext context = new LaboteContext())
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    MenuList = context.MenuModules.ToList();
                }
            }

            #region Dashboard
            var Dashboard = new MenuModule
            {
                IconName = "fa fa-chart-line",
                PageName = "Genel Inceleme",
                OrderNumber = 0,
                IsMainPage = false,
                PageUrl = "dashboard"
            };
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == Dashboard.PageName);
                    if (Ky == null)
                    {
                        context.Add(Dashboard); //////////
                        context.SaveChanges();
                    }
                    else { Dashboard = Ky; }///////

                    transaction.Commit();
                }
            }

            #endregion

            #region Yönetimsel Araçlar

            var KullaniciYonetimi = new MenuModule
            {
                IconName = "fas fa-cogs",
                PageName = "Yönetimsel Araçlar",
                OrderNumber = 1,
                IsMainPage = true,
                PageUrl = "yonetimsel-araclar"
            };
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == KullaniciYonetimi.PageName);
                    if (Ky == null)
                    {
                        context.Add(KullaniciYonetimi);
                        context.SaveChanges();
                    }
                    else { KullaniciYonetimi = Ky; }
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Kullanıcı İşlemleri"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Kullanıcı İşlemleri",
                            PageUrl = "kullanici-islemleri",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 1,
                        });
                    }
                    if (!MenuList.Any(x => x.PageName == "Görev Grupları"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Görev Grupları",
                            PageUrl = "gorev-grup",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 2
                        });
                    }
                    if (!MenuList.Any(x => x.PageName == "Yetki İşlemleri"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Yetki İşlemleri",
                            PageUrl = "yetki-islemleri",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 3
                        });
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            #endregion

            #region Sistem Tanımları
            var Sistem = new MenuModule
            {
                IconName = "fa fa-window-restore",
                PageName = "Sistem & Tanımlama",
                OrderNumber = 1,
                IsMainPage = true,
                PageUrl = "sistem-tanimlama"
            };

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == Sistem.PageName);
                    if (Ky == null)
                    {
                        context.Add(Sistem);
                        context.SaveChanges();
                    }
                    else { Sistem = Ky; }
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    if (!MenuList.Any(x => x.PageName == "Laboratuvar Tanımlama"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Laboratuvar Tanımlama",
                            PageUrl = "laboratuvar-tanimlama",
                            ParentId = Sistem.Id,
                            OrderNumber = 1
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    if (!MenuList.Any(x => x.PageName == "Cihaz Tanımlama"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Cihaz Tanımlama",
                            PageUrl = "cihaz-tanimlama",
                            ParentId = Sistem.Id,
                            OrderNumber = 2
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Analiz Tanımlama"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Analiz Tanımlama",
                            PageUrl = "analiz-tanimlama",
                            ParentId = Sistem.Id,
                            OrderNumber = 3
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            #endregion

            #region Laboratuvar
            var Laboratuvar = new MenuModule
            {
                IconName = "fa fa-flask ",
                PageName = "Laboratuvar",
                OrderNumber = 1,
                IsMainPage = true,
                PageUrl = "laboratuvar"
            };

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == Laboratuvar.PageName);
                    if (Ky == null)
                    {
                        context.Add(Laboratuvar);
                        context.SaveChanges();
                    }
                    else { Laboratuvar = Ky; }
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    if (!MenuList.Any(x => x.PageName == "Numune Kabul"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Numune Kabul",
                            PageUrl = "numune-kabul",
                            ParentId = Laboratuvar.Id,
                            OrderNumber = 1
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    if (!MenuList.Any(x => x.PageName == "Analiz Kayıtları"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Analiz Kayıtları",
                            PageUrl = "analiz-kayitlari",
                            ParentId = Laboratuvar.Id,
                            OrderNumber = 3
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Analiz Oluşturma"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Analiz Oluşturma",
                            PageUrl = "analiz-olusturma",
                            ParentId = Laboratuvar.Id,
                            OrderNumber = 2
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            #endregion

            #region Muhasebe
            var Muhasebe = new MenuModule
            {
                IconName = "fa fa-address-book",
                PageName = "Muhasebe",
                OrderNumber = 1,
                IsMainPage = true,
                PageUrl = "muhasebe"
            };

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == Muhasebe.PageName);
                    if (Ky == null)
                    {
                        context.Add(Muhasebe);
                        context.SaveChanges();
                    }
                    else { Muhasebe = Ky; }
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    if (!MenuList.Any(x => x.PageName == "Cari Hesaplar"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Cari Hesaplar",
                            PageUrl = "cari-hesaplar",
                            ParentId = Muhasebe.Id,
                            OrderNumber = 1
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    if (!MenuList.Any(x => x.PageName == "Faturalar"))
                    {
                        context.Add(new MenuModule
                        {
                            PageName = "Faturalar",
                            PageUrl = "faturalar",
                            ParentId = Muhasebe.Id,
                            OrderNumber = 2
                        });
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }



            #endregion

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    var module = context.MenuModules.Where(x => x.IsMainPage == false && x.ParentId != null && !x.IsHidden).ToList();

                    foreach (var item in module)
                    {
                        var isExist = context.MenuModules.Where(x => x.ParentId == item.Id && x.IsHidden).Any();

                        if (!isExist)
                        {
                            context.MenuModules.Add(new MenuModule
                            {
                                IconName = "icon-pencil",
                                ParentId = item.Id,
                                PageName = "Yazma",
                                IsHidden = true,
                                PageUrl = item.PageUrl

                            });
                        }
                    }
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            return true;
        }
        public async Task<bool> CreateDefaultRoleAsync()
        {
            try
            {
                using (LaboteContext context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransactionAsync())
                    {
                        LaboteUser user = new LaboteUser();

                        if (_roleManager.Roles.Count() == 0)
                        {

                            var userRol = _roleManager.CreateAsync(
                                new UserRole
                                {
                                    Name = Enums.Admin.ToString(),
                                    NormalizedName = Enums.Admin.ToString().ToUpper(),
                                    NotDelete = true,
                                }
                                ).Result;
                        }
                    };

                }
            }
            catch (Exception e)
            {

                throw;
            }



            return true;
        }




    }
}
