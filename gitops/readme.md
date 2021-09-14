## Traffic Court Online deployment on openshift

## Dir Structure

    ├── components                         # for all components for this tco application
    |── environments                       # for test and prod, deploy spec
    ├── network-policy                     # for all network policies for this tco application
    ├── persistent-volume                  # for all components for this tco application
    └── README.md                          # This file.

##Deploy Steps
###Method 1: Manually deploy

1. go to Openshift web console, and login
2. go to the project you want to depoy: 0198bb-test or 0198bb-dev
3. Select Secrets
4. Select "Create secrets"
5. copy the content of gitops\components\secrets\rabbitmq-configuration.yaml to the yaml space in openshift web console
6. do the same thing to create secret gitops\components\secrets\ticket-search-rsi-config.yaml
7. in openshift web console, go to "Config Maps"
8. Create config map for gitops\components\config-map\citizen-web-configuration.yml
9. For each apps, Create Deployment Configs, Services with the files under each apps base folder.
10. For rabbitmq, please refer to readme under messaging.

###Method 2: Use cli command to deploy with templates

1. login

```
oc login --token=your token --server=https://api.silver.devops.gov.bc.ca:6443
```

2.

```
oc project 0198bb-dev
```

3. create secrets

```
oc apply -f .\gitops\components\secrets\rabbitmq-configuration.yaml
oc apply -f .\gitops\components\secrets\ticket-search-rsi-config.yaml
```

5. apply citizen-api

```
cd gitops\components\apps\citizen-api\base\template\
oc process -f .\tco_citizenapi_template.yml --param-file=citizenapi.env | oc create -f -
```

6. apply citizen-web

```
cd gitops\components\apps\citizen-web\base\template
oc process -f .\tco_citizenweb_template.yml --param-file=citizenweb.env | oc create -f -
```

7. apply ticket-search

```
cd gitops\components\apps\ticket-search\base\template
oc process -f .\tco_ticketsearch_template.yml --param-file=ticketsearch.env | oc create -f -
```
