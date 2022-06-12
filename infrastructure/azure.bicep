param servers_semester6_bea_name string = 'semester6-bea'
param vaults_application_secrets_name string = 'application-secrets'
param flexibleServers_hoteldatabase_name string = 'hoteldatabase'
param flexibleServers_bookingdatabase_name string = 'bookingdatabase'
param managedClusters_semester_6_bea_aks_name string = 'semester-6-bea-aks'
param workspaces_defaultworkspace_b3ebf5e0_2c97_4b6a_9f0d_0d4f5bdcdd51_dewc_externalid string = '/subscriptions/b3ebf5e0-2c97-4b6a-9f0d-0d4f5bdcdd51/resourceGroups/defaultresourcegroup-dewc/providers/microsoft.operationalinsights/workspaces/defaultworkspace-b3ebf5e0-2c97-4b6a-9f0d-0d4f5bdcdd51-dewc'
param publicIPAddresses_baa760ff_d9eb_457a_b176_66e17cd4b826_externalid string = '/subscriptions/b3ebf5e0-2c97-4b6a-9f0d-0d4f5bdcdd51/resourceGroups/MC_semester6-bea_semester-6-bea-aks_germanywestcentral/providers/Microsoft.Network/publicIPAddresses/baa760ff-d9eb-457a-b176-66e17cd4b826'
param userAssignedIdentities_semester_6_bea_aks_agentpool_externalid string = '/subscriptions/b3ebf5e0-2c97-4b6a-9f0d-0d4f5bdcdd51/resourceGroups/MC_semester6-bea_semester-6-bea-aks_germanywestcentral/providers/Microsoft.ManagedIdentity/userAssignedIdentities/semester-6-bea-aks-agentpool'

resource managedClusters_semester_6_bea_aks_name_resource 'Microsoft.ContainerService/managedClusters@2022-03-02-preview' = {
  name: managedClusters_semester_6_bea_aks_name
  location: 'germanywestcentral'
  sku: {
    name: 'Basic'
    tier: 'Free'
  }
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    kubernetesVersion: '1.23.5'
    dnsPrefix: '${managedClusters_semester_6_bea_aks_name}-dns'
    agentPoolProfiles: [
      {
        name: 'agentpool'
        count: 2
        vmSize: 'Standard_F2s_v2'
        osDiskSizeGB: 128
        osDiskType: 'Managed'
        kubeletDiskType: 'OS'
        maxPods: 110
        type: 'VirtualMachineScaleSets'
        maxCount: 2
        minCount: 1
        enableAutoScaling: true
        orchestratorVersion: '1.23.5'
        mode: 'System'
        osType: 'Linux'
        osSKU: 'Ubuntu'
      }
    ]
    servicePrincipalProfile: {
      clientId: 'msi'
    }
    addonProfiles: {
      azureKeyvaultSecretsProvider: {
        enabled: true
        config: {
          enableSecretRotation: 'true'
          rotationPollInterval: '2m'
        }
      }
      azurepolicy: {
        enabled: true
      }
      httpApplicationRouting: {
        enabled: true
        config: {
          HTTPApplicationRoutingZoneName: 'd28c7b7324414a4cba3b.germanywestcentral.aksapp.io'
        }
      }
      omsagent: {
        enabled: true
        config: {
          logAnalyticsWorkspaceResourceID: workspaces_defaultworkspace_b3ebf5e0_2c97_4b6a_9f0d_0d4f5bdcdd51_dewc_externalid
        }
      }
    }
    nodeResourceGroup: 'MC_semester6-bea_${managedClusters_semester_6_bea_aks_name}_germanywestcentral'
    enableRBAC: true
    networkProfile: {
      networkPlugin: 'kubenet'
      loadBalancerSku: 'standard'
      loadBalancerProfile: {
        managedOutboundIPs: {
          count: 1
        }
        effectiveOutboundIPs: [
          {
            id: publicIPAddresses_baa760ff_d9eb_457a_b176_66e17cd4b826_externalid
          }
        ]
      }
      podCidr: '10.244.0.0/16'
      serviceCidr: '10.0.0.0/16'
      dnsServiceIP: '10.0.0.10'
      dockerBridgeCidr: '172.17.0.1/16'
      outboundType: 'loadBalancer'
    }
    identityProfile: {
      kubeletidentity: {
        resourceId: userAssignedIdentities_semester_6_bea_aks_agentpool_externalid
        clientId: 'bcb07754-7c99-4952-868e-e129d54da712'
        objectId: '4ca869bb-7fab-493d-a2f0-deba5f0dd211'
      }
    }
    securityProfile: {
      azureDefender: {
        enabled: true
        logAnalyticsWorkspaceResourceId: workspaces_defaultworkspace_b3ebf5e0_2c97_4b6a_9f0d_0d4f5bdcdd51_dewc_externalid
      }
    }
  }
}

resource flexibleServers_bookingdatabase_name_resource 'Microsoft.DBforMySQL/flexibleServers@2021-05-01' = {
  name: flexibleServers_bookingdatabase_name
  location: 'Sweden Central'
  sku: {
    name: 'Standard_B2s'
    tier: 'Burstable'
  }
  properties: {
    administratorLogin: 'adminbea'
    storage: {
      storageSizeGB: 20
      iops: 1280
      autoGrow: 'Enabled'
    }
    version: '5.7'
  }
}

resource flexibleServers_hoteldatabase_name_resource 'Microsoft.DBforMySQL/flexibleServers@2021-05-01' = {
  name: flexibleServers_hoteldatabase_name
  location: 'Sweden Central'
  sku: {
    name: 'Standard_B1ms'
    tier: 'Burstable'
  }
  properties: {
    administratorLogin: 'adminbea'
    storage: {
      storageSizeGB: 20
      iops: 360
      autoGrow: 'Enabled'
    }
    version: '5.7'
  }
}

resource vaults_application_secrets_name_resource 'Microsoft.KeyVault/vaults@2021-11-01-preview' = {
  name: vaults_application_secrets_name
  location: 'northeurope'
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: 'c66b6765-b794-4a2b-84ed-845b341c086a'
    accessPolicies: [
      {
        tenantId: 'c66b6765-b794-4a2b-84ed-845b341c086a'
        objectId: 'c22b7a3b-0c1a-4bc8-baa3-c3c02c4f28eb'
        permissions: {
          keys: [
            'get'
            'list'
            'update'
            'create'
            'import'
            'delete'
            'recover'
            'backup'
            'restore'
            'decrypt'
            'encrypt'
            'unwrapKey'
            'wrapKey'
            'verify'
            'sign'
            'purge'
            'release'
            'rotate'
            'getrotationpolicy'
            'setrotationpolicy'
          ]
          secrets: [
            'get'
            'list'
            'set'
            'delete'
            'recover'
            'backup'
            'restore'
            'purge'
          ]
          certificates: [
            'get'
            'list'
            'update'
            'create'
            'import'
            'delete'
            'recover'
            'backup'
            'restore'
            'managecontacts'
            'manageissuers'
            'getissuers'
            'listissuers'
            'setissuers'
            'deleteissuers'
            'purge'
          ]
        }
      }
      {
        tenantId: 'c66b6765-b794-4a2b-84ed-845b341c086a'
        objectId: 'f19cbec1-584f-4ae3-8687-5f41cbb0af70'
        permissions: {
          keys: [
            'get'
            'list'
            'update'
            'create'
            'import'
            'delete'
            'recover'
            'backup'
            'restore'
            'getrotationpolicy'
            'setrotationpolicy'
            'rotate'
            'release'
            'purge'
            'sign'
            'verify'
            'wrapKey'
            'unwrapKey'
            'encrypt'
            'decrypt'
          ]
          secrets: [
            'get'
            'list'
            'set'
            'delete'
            'recover'
            'backup'
            'restore'
            'purge'
          ]
          certificates: [
            'get'
            'list'
            'update'
            'create'
            'import'
            'delete'
            'recover'
            'backup'
            'restore'
            'managecontacts'
            'manageissuers'
            'getissuers'
            'listissuers'
            'setissuers'
            'deleteissuers'
            'purge'
          ]
        }
      }
    ]
    enabledForDeployment: true
    enabledForDiskEncryption: true
    enabledForTemplateDeployment: true
    publicNetworkAccess: 'Enabled'
  }
}

resource managedClusters_semester_6_bea_aks_name_agentpool 'Microsoft.ContainerService/managedClusters/agentPools@2022-03-02-preview' = {
  parent: managedClusters_semester_6_bea_aks_name_resource
  name: 'agentpool'
  properties: {
    count: 2
    vmSize: 'Standard_F2s_v2'
    osDiskSizeGB: 128
    osDiskType: 'Managed'
    kubeletDiskType: 'OS'
    maxPods: 110
    type: 'VirtualMachineScaleSets'
    maxCount: 2
    minCount: 1
    enableAutoScaling: true
    orchestratorVersion: '1.23.5'
    mode: 'System'
    osType: 'Linux'
    osSKU: 'Ubuntu'
  }
}
