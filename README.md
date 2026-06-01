# Inventra

Inventra, modern .NET teknolojileri ve Onion Architecture prensipleri kullanılarak geliştirilen bir Envanter Yönetim API projesidir.

Bu projenin amacı; gerçek dünya senaryolarına yakın bir sistem geliştirirken CQRS, MediatR, Minimal API, FluentValidation, Unit of Work ve diğer modern backend yaklaşımlarını uygulamalı olarak kullanmaktır.

## Kullanılan Teknolojiler

* ASP.NET Core 9
* Minimal API
* Entity Framework Core
* SQL Server
* MediatR
* FluentValidation
* Mapster
* Onion Architecture

## Mimari

Proje Onion Architecture yaklaşımıyla geliştirilmiştir.

```text
Inventra.API
Inventra.Application
Inventra.Domain
Inventra.Persistence
```

### Domain Katmanı

* Entity'ler
* Enum'lar
* Domain modelleri

### Application Katmanı

* CQRS (Command & Query)
* MediatR Handler'ları
* FluentValidation doğrulamaları
* Result Pattern
* Repository soyutlamaları
* Unit Of Work soyutlamaları

### Persistence Katmanı

* Entity Framework Core
* Repository implementasyonları
* DbContext
* Entity Configuration'ları
* Audit Interceptor

### API Katmanı

* Minimal API Endpoint'leri
* Middleware'ler
* OpenAPI / Scalar yapılandırmaları

## Tamamlanan Özellikler

### Ürün Yönetimi

* Ürün oluşturma
* Ürün listeleme
* Id ile ürün getirme
* Ürün güncelleme
* Ürün silme

### Doğrulama Altyapısı

FluentValidation ve MediatR Pipeline Behavior kullanılarak merkezi doğrulama mekanizması oluşturulmuştur.

### Global Exception Handling

Uygulama genelinde oluşan hatalar merkezi bir middleware üzerinden yönetilmektedir.

### Audit Altyapısı

EF Core Interceptor kullanılarak aşağıdaki alanlar otomatik olarak yönetilmektedir:

* CreatedDate
* UpdatedDate

## Veritabanı

Proje SQL Server kullanmaktadır.

### Migration Oluşturma

```bash
dotnet ef migrations add InitialCreate -p Inventra.Persistence -s Inventra.API
```

### Veritabanını Güncelleme

```bash
dotnet ef database update -p Inventra.Persistence -s Inventra.API
```

## API Dokümantasyonu

Proje OpenAPI ve Scalar kullanmaktadır.

Uygulama çalıştırıldıktan sonra aşağıdaki adresten API dokümantasyonuna erişilebilir:

```text
/scalar/v1
```

## Yol Haritası

* [x] Product CRUD
* [ ] Warehouse Yönetimi
* [ ] Stock Yönetimi
* [ ] Stock Movement Yönetimi
* [ ] Supplier Yönetimi
* [ ] Purchase Order Yönetimi
* [ ] Raporlama Modülü

## Durum

Proje aktif olarak geliştirilmektedir ve yeni modüller eklenmeye devam edilmektedir.
