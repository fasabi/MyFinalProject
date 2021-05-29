using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi.";
        public static string ProductNotAdded = "Ürün eklenemedi.";
        public static string ProductNameInvalid = "Ürün ismi geçersizdir.";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductListed = "Ürünler listelendi.";
        public static string ProductNotListed = "Ürünler listelenemedi.";
        public static string GetByUnitPriceListed = "Belirtilen birim fiyatı aralığındaki ürünler listelendi.";
        public static string GetByUnitPriceNotListed = "Belirtilen birim fiyatı aralığındaki ürünler listelenemedi !";
        public static string ProductUpdated = "Ürün güncellendi.";
        public static string ProductNotUpdated = "Ürün güncellenemedi.";
        public static string ProductDeleted = "Ürün silindi.";
        public static string ProductNotDeleted = "Ürün silinemedi.";

        public static string CategoryAdded = "Kategori eklendi.";
        public static string CategoryNotAdded = "Kategori eklenemedi.";
        public static string CategoryNameInvalid = "Kategori ismi geçersizdir.";
        public static string CategoryListed = "Kategoriler listelendi.";
        public static string CategoryNotListed = "Kategoriler listelenemedi.";
        public static string CategoryUpdated = "Kategori güncellendi.";
        public static string CategoryNotUpdated = "Kategori güncellenemedi.";
        public static string CategoryDeleted = "Kategori silindi.";
        public static string CategoryNotDeleted = "Kategori silinemedi.";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";

    }
}
