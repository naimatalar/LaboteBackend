using Labote.Core.BindingModels;
using Labote.Core.Constants;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public interface IAntegraContextDbSeed
    {
        Task<bool> CreateRoleAndUsersAsync();
        Task<bool> CreateDefaultRole();
        Task<bool> MenuSeed();

    }

    public class AntegraContextDbSeed : IAntegraContextDbSeed
    {
        private readonly UserManager<AntegraUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly AntegraContext _antegraContext;
        public AntegraContextDbSeed(UserManager<AntegraUser> userManager, RoleManager<UserRole> roleManager, AntegraContext antegraContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _antegraContext = antegraContext;
        }



        public async Task<bool> CreateRoleAndUsersAsync()
        {
            try
            {
                using (AntegraContext context = new AntegraContext())
                {
                    using (var transaction = context.Database.BeginTransactionAsync())
                    {
                        AntegraUser user = new AntegraUser();
                        if (_userManager.Users.Count() == 0)
                        {
                            user = new AntegraUser()
                            {
                                Email = "mail@mail.com",
                                SecurityStamp = Guid.NewGuid().ToString(),
                                UserName = "Varsayilan",
                                FirstName = "Varsayılan",
                                Lastname = "Kullanici",
                                EmailConfirmed = true,
                            };
                            var usr = _userManager.CreateAsync(user, "Tg71!nG*").Result;
                        }
                        if (_roleManager.Roles.Count() == 0)
                        {
                            var admninRol = _roleManager.CreateAsync(new UserRole
                            {
                                Name = Enums.Admin.ToString(),
                                NormalizedName = Enums.Admin.ToString().ToUpper()

                            }).Result;
                            var userRol = _roleManager.CreateAsync(
                                new UserRole
                                {
                                    Name = Enums.User.ToString(),
                                    NormalizedName = Enums.User.ToString().ToUpper()
                                }
                                ).Result;
                        }
                        var RoleAdd = _userManager.AddToRoleAsync(user, Enums.Admin).Result;
                    };

                }
            }
            catch (Exception e)
            {

                throw;
            }



            return true;
        }
        public async Task<bool> MenuSeed()
        {
            var MenuList = _antegraContext.MenuModules.ToList();
            #region Dashboard
            var Dashboard = new MenuModule
            {
                IconName = "icon-home4",
                PageName = "Dahsboard",
                OrderNumber = 0,
                IsMainPage = false,
                PageUrl = "dashboard"
            };
            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == Dashboard.PageName);
                    if (Ky == null)
                    {
                        _antegraContext.Add(Dashboard); //////////
                        _antegraContext.SaveChanges();
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
            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == KullaniciYonetimi.PageName);
                    if (Ky == null)
                    {
                        _antegraContext.Add(KullaniciYonetimi);
                        _antegraContext.SaveChanges();
                    }
                    else { KullaniciYonetimi = Ky; }
                    transaction.Commit();
                }
            }
            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Kullanıcı Oluştur"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Kullanıcı Oluştur",
                            PageUrl = "kullanici-olustur",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 1,
                        });
                    }
                    if (!MenuList.Any(x => x.PageName == "Grup Tanımları"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Grup Tanımları",
                            PageUrl = "grup-tanimlari",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 2
                        });
                    }
                    if (!MenuList.Any(x => x.PageName == "Yetki Tanımları"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Yetki Tanımları",
                            PageUrl = "yetki-tanimlari",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 3
                        });
                    }
                    if (!MenuList.Any(x => x.PageName == "Bayi Tanımları"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Bayi Tanımları",
                            PageUrl = "bayi-tanimlari",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 4
                        });
                    }

                    if (!MenuList.Any(x => x.PageName == "ERP Entegrasyonu"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Erp Entegrasyonu",
                            PageUrl = "erp-entegrasyonu",
                            ParentId = KullaniciYonetimi.Id,
                            OrderNumber = 5
                        });
                    }


                    _antegraContext.SaveChanges();
                    transaction.Commit();
                }
            }
            #endregion

            #region Sanalpazar Yönetimi 
            var SanalPazarYonetim = new MenuModule
            {
                IconName = "icon-cart",
                PageName = "Sanal Pazar Yönetimi",
                OrderNumber = 2,
                IsMainPage = true,
                PageUrl = "sanal-pazar-yonetimi"
            };

            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == SanalPazarYonetim.PageName);/////
                    if (Ky == null)
                    {
                        _antegraContext.Add(SanalPazarYonetim); //////////
                        _antegraContext.SaveChanges();
                    }
                    else { SanalPazarYonetim = Ky; }///////
                    transaction.Commit();
                }
            }

            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Sanal Pazar Entegrasyon"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Sanal Pazar Entegrasyon",
                            PageUrl = "sanal-pazar-entegrasyon",
                            ParentId = SanalPazarYonetim.Id,
                            OrderNumber = 1,
                        });
                    }

                    _antegraContext.SaveChanges();
                    transaction.Commit();
                }
            }
            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Sanal Pazar Analiz"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Sanal Pazar Analiz",
                            PageUrl = "sanal-pazar-analiz",
                            ParentId = SanalPazarYonetim.Id,
                            OrderNumber = 1,
                        });
                    }

                    _antegraContext.SaveChanges();
                    transaction.Commit();
                }
            }

            #endregion

            #region Ürün Yönetimi  
            var UrunYonetimi = new MenuModule
            {
                IconName = "fas fa-barcode",
                PageName = "Ürün Yönetimi",
                OrderNumber = 2,
                IsMainPage = true,
                PageUrl = "urun-yonetimi"
            };

            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var Ky = MenuList.FirstOrDefault(x => x.PageName == UrunYonetimi.PageName);/////
                    if (Ky == null)
                    {
                        _antegraContext.Add(UrunYonetimi); //////////
                        _antegraContext.SaveChanges();
                    }
                    else { UrunYonetimi = Ky; }///////
                    transaction.Commit();
                }
            }

            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Ürün Listesi"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Ürün Listesi",
                            PageUrl = "urun-listesi",
                            ParentId = UrunYonetimi.Id,
                            OrderNumber = 1,
                        });
                    }

                    _antegraContext.SaveChanges();
                    transaction.Commit();
                }
            }

            using (AntegraContext context = new AntegraContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!MenuList.Any(x => x.PageName == "Ürün Kategori"))
                    {
                        _antegraContext.Add(new MenuModule
                        {
                            PageName = "Ürün Kategori",
                            PageUrl = "urun-kategori",
                            ParentId = UrunYonetimi.Id,
                            OrderNumber = 2,
                        });
                    }

                    _antegraContext.SaveChanges();
                    transaction.Commit();
                }
            }
            #endregion




            using (AntegraContext context = new AntegraContext())
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
        public async Task<bool> CreateDefaultRole()
        {
            try
            {
                using (AntegraContext context = new AntegraContext())
                {
                    using (var transaction = context.Database.BeginTransactionAsync())
                    {
                        AntegraUser user = new AntegraUser();

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
