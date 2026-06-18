# 🚀 Inventra

> Modern .NET teknolojileri kullanılarak geliştirilen, yapay zekâ destekli Envanter ve Stok Yönetim Sistemi.

Inventra; kurumsal envanter süreçlerini dijital ortamda yönetmek amacıyla geliştirilmiş, Onion Architecture prensiplerini temel alan modern bir stok yönetim uygulamasıdır.

Proje yalnızca temel CRUD işlemlerini değil; CQRS, Event-Driven Architecture, gerçek zamanlı bildirimler ve yapay zekâ destekli analizler gibi güncel yazılım yaklaşımlarını da uygulamalı olarak içermektedir.

---

# ✨ Öne Çıkan Özellikler

* 📦 Ürün Yönetimi
* 🏢 Depo Yönetimi
* 🤝 Tedarikçi Yönetimi
* 📊 Stok Yönetimi
* 🔄 Stok Hareketleri
* 🛒 Satın Alma Siparişleri
* 🔔 SignalR ile gerçek zamanlı bildirimler
* 🤖 OpenAI destekli satın alma analizi
* 📝 Audit Log altyapısı
* ✅ FluentValidation Pipeline
* ⚠️ Global Exception Middleware
* 📈 Dashboard ve raporlama ekranları

---

# 🖼️ Uygulama Görselleri

> Dashboard

> Ürün Yönetimi

> Stok Yönetimi

> Satın Alma Siparişi

> Yapay Zekâ Analizi

(Buraya ekran görüntüleri eklenecek.)

---

# 🏗️ Kullanılan Teknolojiler

## Backend

* ASP.NET Core 9
* Minimal API
* Entity Framework Core
* SQL Server
* MediatR
* CQRS
* FluentValidation
* Mapster
* Repository Pattern
* Unit of Work Pattern
* Result Pattern
* Onion Architecture

## Frontend

* ASP.NET Core MVC
* Razor Views
* Bootstrap 5
* HttpClient

## Messaging & AI

* SignalR
* RabbitMQ
* MassTransit
* OpenAI API

---

# 📁 Proje Yapısı

```text
Inventra.API
Inventra.Application
Inventra.Domain
Inventra.Persistence
Inventra.WebUI
```

## Domain

* Entity'ler
* Enum'lar
* Domain modelleri

## Application

* CQRS
* MediatR
* Validation
* Repository Abstractions
* Unit of Work
* Result Pattern

## Persistence

* Entity Framework Core
* Repository Implementasyonları
* DbContext
* Entity Configurations
* Audit Interceptor

## API

* Minimal API
* OpenAPI
* Scalar
* Middleware

## WebUI

* ASP.NET Core MVC
* Razor Views
* Dashboard
* Yönetim Panelleri

---

# 📦 Modüller

## Ürün Yönetimi

* Ürün oluşturma
* Güncelleme
* Silme
* Listeleme
* Detay görüntüleme

## Depo Yönetimi

* Depo oluşturma
* Güncelleme
* Silme
* Listeleme

## Tedarikçi Yönetimi

* Tedarikçi oluşturma
* Güncelleme
* Silme
* Listeleme

## Stok Yönetimi

* Stock In
* Stock Out
* Warehouse Transfer
* Stok sorgulama

## Stok Hareketleri

* Hareket geçmişi
* Giriş
* Çıkış
* Transfer hareketleri

## Satın Alma Siparişleri

* Sipariş oluşturma
* Onaylama
* Tamamlama
* Sipariş durum yönetimi
* Yapay zekâ destekli satın alma analizi

## Dashboard

* Toplam ürün
* Toplam depo
* Toplam tedarikçi
* Toplam stok
* Kritik stoklar
* Son stok hareketleri
* Bekleyen siparişler

---

# 🤖 Yapay Zekâ Analizi

Inventra, OpenAI entegrasyonu sayesinde satın alma siparişlerini analiz edebilmektedir.

Analiz sırasında;

* Mevcut stok durumu
* Minimum stok seviyesi
* Sipariş miktarı
* Sipariş tutarı
* Ürün bazlı değerlendirmeler

incelenerek satın alma süreci hakkında kullanıcıya öneriler sunulmaktadır.

---

# 🛡️ Altyapı Özellikleri

* FluentValidation Pipeline
* Global Exception Middleware
* Audit Interceptor
* Result Pattern
* Role Based Authorization
* JWT Authentication
* Health Check
* Structured Logging

---

# 💾 Veritabanı

SQL Server kullanılmaktadır.

```bash
dotnet ef migrations add InitialCreate -p Inventra.Persistence -s Inventra.API
```

```bash
dotnet ef database update -p Inventra.Persistence -s Inventra.API
```

---

# 📄 API Dokümantasyonu

Scalar arayüzüne aşağıdaki adresten erişebilirsiniz.

```text
/scalar/v1
```

---

# 🎯 Projenin Amacı

Inventra, modern .NET ekosisteminde kullanılan mimari desenleri, katmanlı yapı yaklaşımını ve gerçek dünya envanter süreçlerini tek bir proje altında bir araya getirmek amacıyla geliştirilmiştir.

Bu proje sayesinde CQRS, Onion Architecture, SignalR, RabbitMQ, MassTransit ve OpenAI entegrasyonu gibi birçok teknolojinin birlikte nasıl kullanılabileceği uygulamalı olarak gösterilmektedir.
