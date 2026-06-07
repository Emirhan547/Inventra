# Inventra

Inventra, modern .NET teknolojileri ve Onion Architecture prensipleri kullanılarak geliştirilen bir Envanter ve Stok Yönetim Sistemidir.

Proje; gerçek dünya senaryolarına yakın bir yapı oluşturmak amacıyla geliştirilmiş olup CQRS, MediatR, Minimal API, Entity Framework Core, FluentValidation, Result Pattern ve modern katmanlı mimari yaklaşımlarını uygulamalı olarak içermektedir.

---

# Kullanılan Teknolojiler

### Backend

* ASP.NET Core 9
* Minimal API
* Entity Framework Core
* SQL Server
* MediatR
* FluentValidation
* Mapster
* Onion Architecture
* CQRS Pattern
* Repository Pattern
* Unit Of Work Pattern
* Result Pattern

### Frontend

* ASP.NET Core MVC
* Razor Views
* Bootstrap
* HttpClient

---

# Mimari

Proje Onion Architecture yaklaşımıyla geliştirilmiştir.

```text
Inventra.API
Inventra.Application
Inventra.Domain
Inventra.Persistence
Inventra.WebUI
```

## Domain Katmanı

* Entity'ler
* Enum'lar
* Domain Modelleri

## Application Katmanı

* CQRS (Command & Query)
* MediatR Handler'ları
* FluentValidation
* Result Pattern
* Repository Soyutlamaları
* Unit Of Work Soyutlamaları

## Persistence Katmanı

* Entity Framework Core
* Repository Implementasyonları
* DbContext
* Entity Configuration'ları
* Audit Interceptor

## API Katmanı

* Minimal API Endpoint'leri
* Middleware'ler
* OpenAPI
* Scalar

## WebUI Katmanı

* ASP.NET Core MVC
* Razor View'lar
* Dashboard
* Yönetim Panelleri
* API Tüketimi

---

# Tamamlanan Modüller

## Kategori Yönetimi

* Kategori oluşturma
* Kategori listeleme
* Kategori güncelleme
* Kategori silme

## Ürün Yönetimi

* Ürün oluşturma
* Ürün listeleme
* Ürün detay görüntüleme
* Ürün güncelleme
* Ürün silme

## Depo Yönetimi

* Depo oluşturma
* Depo listeleme
* Depo güncelleme
* Depo silme

## Tedarikçi Yönetimi

* Tedarikçi oluşturma
* Tedarikçi listeleme
* Tedarikçi güncelleme
* Tedarikçi silme

## Stok Yönetimi

* Stock In
* Stock Out
* Depolar arası transfer
* Stok sorgulama

## Stok Hareketleri

* Hareket geçmişi görüntüleme
* Giriş hareketleri
* Çıkış hareketleri
* Transfer hareketleri

## Satın Alma Yönetimi

* Satın alma siparişi oluşturma
* Sipariş detay görüntüleme
* Sipariş onaylama
* Sipariş tamamlama
* Sipariş durum yönetimi

## Dashboard

* Toplam ürün sayısı
* Toplam depo sayısı
* Toplam tedarikçi sayısı
* Toplam stok kaydı
* Toplam stok hareketi
* Toplam satın alma siparişi
* Bekleyen sipariş sayısı
* Son stok hareketleri
* Kritik stoklar

---

# Altyapı Özellikleri

## Validation Pipeline

FluentValidation ve MediatR Pipeline Behavior kullanılarak merkezi doğrulama mekanizması oluşturulmuştur.

## Global Exception Handling

Uygulama genelinde oluşan hatalar merkezi middleware üzerinden yönetilmektedir.

## Audit Altyapısı

EF Core Interceptor kullanılarak aşağıdaki alanlar otomatik yönetilmektedir:

* CreatedDate
* UpdatedDate

## Result Pattern

Tüm operasyonlar standart sonuç yapısı üzerinden yönetilmektedir.

---

# Veritabanı

Proje SQL Server kullanmaktadır.

## Migration Oluşturma

```bash
dotnet ef migrations add InitialCreate -p Inventra.Persistence -s Inventra.API
```

## Veritabanını Güncelleme

```bash
dotnet ef database update -p Inventra.Persistence -s Inventra.API
```

---

# API Dokümantasyonu

Proje OpenAPI ve Scalar kullanmaktadır.

Uygulama çalıştırıldıktan sonra:

```text
/scalar/v1
```

adresinden API dokümantasyonuna erişilebilir.

---

# Yol Haritası

* ASP.NET Core Identity
* JWT Authentication
* Refresh Token
* Role Management
* Policy Based Authorization
* SignalR Entegrasyonu
* RabbitMQ Entegrasyonu
* Logging & Monitoring
* Cache Mekanizması
* Docker Desteği

---

# Durum

Proje aktif olarak geliştirilmektedir ve yeni modüller eklenmeye devam etmektedir.
