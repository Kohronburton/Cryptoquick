# Cryptoquick

> **Historical portfolio project — 2019**  
> Retained to document an earlier stage of my engineering work with C#, Xamarin, cross-platform mobile applications, and Azure Cosmos DB.

## What this repository demonstrates

This solution contains a shared Xamarin project with Android and iOS targets. The application models a small item workflow backed by Azure Cosmos DB and demonstrates:

- shared C# application logic
- Xamarin/XAML user-interface structure
- Android and iOS platform projects
- asynchronous Cosmos DB queries and writes
- create, retrieve, and complete operations
- separation between the application page, model, and data manager

## Why it remains public

This repository is part of my engineering timeline. It shows the technologies and implementation patterns I was working with before my current focus on full-stack systems, cloud architecture, deterministic business engines, and AI workflows.

It is presented as historical evidence—not as a current production dependency.

## Security correction

The original 2019 version contained a Cosmos DB credential in source code. The current branch reads configuration from:

- `CRYPTOQUICK_COSMOS_URL`
- `CRYPTOQUICK_COSMOS_KEY`

Any credential previously committed must be treated as compromised, rotated in Azure, and removed from Git history before this repository is reused.

## What I would change today

A production rebuild would use:

- a backend API instead of direct database access from the mobile client
- managed identity or a server-side secret store
- least-privilege authorization
- configuration validation at startup
- modern .NET MAUI or a supported web/mobile stack
- automated tests and CI
- structured logging and telemetry
- dependency and secret scanning

## Status

Historical, unsupported, and retained for portfolio context. Do not use the old dependencies or credentials in production.
