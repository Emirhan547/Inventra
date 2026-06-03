# Inventra

Inventra, modern .NET teknolojileri ve Onion Architecture prensipleri kullanılarak geliştirilen bir Envanter Yönetim API projesidir.

Bu projenin amacı; gerçek dünya senaryolarına yakın bir sistem geliştirirken CQRS, MediatR, Minimal API, FluentValidation, Unit Of Work ve diğer modern backend yaklaşımlarını uygulamalı olarak kullanmaktır.

---

# Kullanılan Teknolojiler

* ASP.NET Core 9
* Minimal API
* Entity Framework Core
* SQL Server
* MediatR
* FluentValidation
* Mapster
* Onion Architecture
* CQRS Pattern
* Result Pattern
* Unit Of Work Pattern

---

# Mimari

Proje Onion Architecture yaklaşımıyla geliştirilmiştir.

```text
Inventra.API
Inventra.Application
Inventra.Domain
Inventra.Persistence
```

## Domain Katmanı

* Entity'ler
* Enum'lar
* Domain Modelleri

## Application Katmanı

* CQRS (Command & Query)
* MediatR Handler'ları
* FluentValidation Doğrulamaları
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
* OpenAPI / Scalar Yapılandırmaları

---

# Tamamlanan Özellikler

## Ürün Yönetimi

* Ürün oluşturma
* Ürün listeleme
* Id ile ürün getirme
* Ürün güncelleme
* Ürün silme

## Depo Yönetimi

* Depo oluşturma
* Depo listeleme
* Id ile depo getirme
* Depo güncelleme
* Depo silme

## Tedarikçi Yönetimi

* Tedarikçi oluşturma
* Tedarikçi listeleme
* Id ile tedarikçi getirme
* Tedarikçi güncelleme
* Tedarikçi silme

## Stok Yönetimi

* Stok girişi (Stock In)
* Stok çıkışı (Stock Out)
* Depolar arası stok transferi
* Stok sorgulama

## Stok Hareketleri

* Stock In hareketleri
* Stock Out hareketleri
* Transfer hareketleri
* Hareket geçmişi görüntüleme

## Dashboard

* Toplam ürün sayısı
* Toplam depo sayısı
* Toplam stok kaydı sayısı
* Toplam stok hareketi sayısı

---

# Altyapı Özellikleri

## Doğrulama Altyapısı

FluentValidation ve MediatR Pipeline Behavior kullanılarak merkezi doğrulama mekanizması oluşturulmuştur.

## Global Exception Handling

Uygulama genelinde oluşan hatalar merkezi bir middleware üzerinden yönetilmektedir.

## Audit Altyapısı

EF Core Interceptor kullanılarak aşağıdaki alanlar otomatik olarak yönetilmektedir:

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

Uygulama çalıştırıldıktan sonra aşağıdaki adresten API dokümantasyonuna erişilebilir:

```text
/scalar/v1
```

---

# Yol Haritası

* Purchase Order Yönetimi
* Purchase Order Item Yönetimi
* Sipariş Teslim Alma Süreci
* SignalR Entegrasyonu
* RabbitMQ Entegrasyonu
* Logging & Monitoring
* Cache Mekanizması
* Authentication & Authorization
* Web UI

---

# Durum

Proje aktif olarak geliştirilmektedir ve yeni modüller eklenmeye devam edilmektedir.
