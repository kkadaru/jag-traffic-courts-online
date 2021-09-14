## Traffic Court Online deployment on openshift

## Dir Structure

    ├── components                         # for all components for this tco application
    |── environments                       # for test and prod, deploy spec
    ├── network-policy                     # for all network policies for this tco application
    ├── persistent-volume                  # for all components for this tco application
    └── README.md                          # This file.

go to folder to each components,

template
for example:
components/app/citizen-api/base/template
run

```
oc process -f tco_citizenapi_template.yml --param-file=citizenapi.env | oc create -f -
```
