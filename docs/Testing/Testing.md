# Testing Overview

Tests are organized into:

### Unit Tests

* Validate matrix logic (`CardResolver`, rule evaluation)
* Lightweight, fast feedback cycle

### Integration Tests

* Cover middleware behavior and full request/response flow
* Validate security layers, headers, and error responses

### Stack

* **Framework**: xUnit
* **Mocking**: Moq
* **Setup**: Custom `WebApplicationFactory`

Test coverage ensures all critical paths and edge cases are verified automatically.
