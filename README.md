# warehouseapi

## Version: 1.0

### /api/Item

#### GET

##### Responses

| Code | Description |
| ---- | ----------- |
| 200  | Success     |

#### POST

##### Responses

| Code | Description |
| ---- | ----------- |
| 200  | Success     |

### /api/Item/{id}

#### GET

##### Parameters

| Name | Located in | Description | Required | Schema        |
| ---- | ---------- | ----------- | -------- | ------------- |
| id   | path       |             | Yes      | string (uuid) |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200  | Success     |

#### PUT

##### Parameters

| Name | Located in | Description | Required | Schema        |
| ---- | ---------- | ----------- | -------- | ------------- |
| id   | path       |             | Yes      | string (uuid) |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200  | Success     |

#### DELETE

##### Parameters

| Name | Located in | Description | Required | Schema        |
| ---- | ---------- | ----------- | -------- | ------------- |
| id   | path       |             | Yes      | string (uuid) |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200  | Success     |

### Models

#### ItemDTO

| Name        | Type   | Description | Required |
| ----------- | ------ | ----------- | -------- |
| name        | string |             | Yes      |
| description | string |             | Yes      |
| quantity    | float  |             | Yes      |
| price       | float  |             | Yes      |
