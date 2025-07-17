# Infonetica-Task
This repository is the implementation of state-machine API


## Feautures

- Define one or more configurable workflows (states + actions)
- Start workflow instances from predefined workflows
- Execute actions to transition instances with validation
- Inspect and list definitions, states, actions, and instances 

## API Endpoints
| Method | Endpoint                                 | Description                       |
|--------|------------------------------------------|-----------------------------------|
| POST   | `/workflows`                             | Create a new workflow definition  |
| GET    | `/workflows/{id}`                        | Retrieve a workflow definition    |
| POST   | `/workflows/instances`                   | Start a new workflow instance     |
| POST   | `/workflows/action`                      | Execute an action/transition      |
| GET    | `/workflows/instances/{id}`              | Retrieve  workflow instance by ID |

### Usage

- /workflows
![/workflows](images/4.png)

```
Define a workflow with its possible states and their properties.
```

- /workflows/{id}
![/workflows/{id}](images/3.png)

```
Fetch the states and actions associated with a workflow.
```

- /workflows/instances
![/workflows/instances](images/1.png)
```
Create a new instance of a workflow with an initial state.
```
- /workflows/action
![/workflows/action](images/2.png)

```
Triggers a transition from a current state to a new state if the action is valid.
```