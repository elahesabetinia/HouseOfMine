using HouseOfMine.Domain.Entities;
using HouseOfMine.Domain.Enums;
using HouseOfMine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HouseOfMine.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Provinces.AnyAsync()) return; // قبلاً Seed شده

        // ۱) استان‌ها
        var tehranProv = new Province("تهران");
        var isfahanProv = new Province("اصفهان");
        var farsProv = new Province("فارس");
        db.Provinces.AddRange(tehranProv, isfahanProv, farsProv);
        await db.SaveChangesAsync();

        // ۲) شهرها
        var tehran = new City("تهران", tehranProv.Id);
        var isfahan = new City("اصفهان", isfahanProv.Id);
        var shiraz = new City("شیراز", farsProv.Id);
        db.Cities.AddRange(tehran, isfahan, shiraz);

        // ۳) شغل‌ها و انواع ملک
        var engineer = new Occupation("مهندس");
        var teacher = new Occupation("معلم");
        var merchant = new Occupation("بازرگان");
        db.Occupations.AddRange(engineer, teacher, merchant);

        var apartment = new PropertyType("آپارتمان");
        var land = new PropertyType("زمین");
        var shop = new PropertyType("مغازه");
        db.PropertyTypes.AddRange(apartment, land, shop);
        await db.SaveChangesAsync();

        // ۴) شهروندان (کد ملی از روی ۹ رقم پایه با رقم کنترل درست ساخته می‌شود)
        var ali = new Citizen(MakeCode("123456789"), "علی", "رضایی", "محمد",
            Gender.Male, MaritalStatus.Married, tehran.Id, new DateOnly(1985, 3, 12), engineer.Id);
        var maryam = new Citizen(MakeCode("234567890"), "مریم", "احمدی", "حسین",
            Gender.Female, MaritalStatus.Single, isfahan.Id, new DateOnly(1992, 7, 25), teacher.Id);
        var reza = new Citizen(MakeCode("345678901"), "رضا", "کریمی", "عباس",
            Gender.Male, MaritalStatus.Married, shiraz.Id, new DateOnly(1978, 11, 2), merchant.Id);
        db.Citizens.AddRange(ali, maryam, reza);

        // ۵) املاک
        var p1 = new Property("1204/55", apartment.Id, tehran.Id,
            "تهران، خیابان ولیعصر، کوچه ۱۲، پلاک ۸، واحد ۳", "1967812345", 95.5m);
        var p2 = new Property("873/12", land.Id, isfahan.Id,
            "اصفهان، شهرک صنعتی، قطعه ۴۲", "8173912345", 400m);
        var p3 = new Property("2210/7", shop.Id, shiraz.Id,
            "شیراز، بلوار زند، پلاک ۱۵۰", "7134812345", 38m);
        db.Properties.AddRange(p1, p2, p3);
        await db.SaveChangesAsync();

        // ۶) مالکیت‌ها (ملک اول مشترک بین دو نفر است)
        db.Ownerships.AddRange(
            new Ownership(p1.Id, ali.Id, 4, new DateOnly(2015, 6, 1)),
            new Ownership(p1.Id, maryam.Id, 2, new DateOnly(2018, 1, 15)),
            new Ownership(p2.Id, maryam.Id, 6, new DateOnly(2020, 9, 10)),
            new Ownership(p3.Id, reza.Id, 6, new DateOnly(2010, 4, 5)));
        await db.SaveChangesAsync();
    }

    // ۹ رقم پایه را می‌گیرد، رقم کنترل را اضافه می‌کند و NationalCode معتبر می‌سازد
    private static NationalCode MakeCode(string nineDigits)
    {
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (nineDigits[i] - '0') * (10 - i);

        int r = sum % 11;
        int check = r < 2 ? r : 11 - r;
        return NationalCode.Create(nineDigits + check);
    }
}