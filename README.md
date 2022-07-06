# Warehouse API
An ASP.NET Core Web API for managing warehouse items

## Version: v1

**Contact information:**  
Paweł Bąk  
<https://github.com/bakpawel103>  

### /api/Item

#### GET
##### Summary

Gets all Items.

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Successful operation |
| 500 | Internal server error |

#### POST
##### Summary

Creates an Item.

##### Responses

| Code | Description |
| ---- | ----------- |
| 201 | Created a new item. |
| 500 | Internal server error. |

### /api/Item/{id}

#### GET
##### Summary

Gets a specific Item.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Id of the searching item | Yes | string (uuid) |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Successful operation |
| 500 | Internal server error |

#### PUT
##### Summary

Updates a specific Item.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Id of the searching item | Yes | string (uuid) |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Updated a specific item. |
| 404 | Can't fint specific item. |
| 500 | Internal server error. |

#### DELETE
##### Summary

Deletes a specific Item.

##### Parameters

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | path | Id of the searching item | Yes | string (uuid) |

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Successful operation. |
| 500 | Internal server error. |

### Models

#### Item

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| id | string (uuid) | The unique id of the item<br>_Example:_ `"500fccb6-9844-497e-8edc-dbafadcb1133"` | No |
| name | string | The name of the item<br>_Example:_ `"Small box"` | No |
| createDate | dateTime | The creation date of the item<br>_Example:_ `"04/07/2022 15:41:33"` | No |
| description | string | The description of the item<br>_Example:_ `"A Small box"` | No |
| quantity | integer | The quantity of the item<br>_Example:_ `2` | No |
| price | float | The price of the item<br>_Example:_ `12.5` | No |

#### ItemDTO

| Name | Type | Description | Required |
| ---- | ---- | ----------- | -------- |
| name | string | The name of the item<br>_Example:_ `"Small box"` | Yes |
| description | string | The description of the item<br>_Example:_ `"A Small box"` | Yes |
| quantity | integer | The quantity of the item<br>_Example:_ `2` | Yes |
| price | float | The price of the item<br>_Example:_ `12.5` | Yes |
