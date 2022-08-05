<template>

    <div class=" mb-2 mx-3 p-0 shadow rounded selectable" @click="goToVaultPage">
        <div class=" ">
            <img class=" rounded img-tag" :src="vault.img" alt="">
        </div>
        <div class="text-img d-flex justify-content-between text-light">
            <h4>{{ vault.name }}</h4>

        </div>
    </div>

</template >
    
    
    <script>

import { useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {

    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        const router = useRouter()
        return {
            router,
            async goToVaultPage() {
                try {

                    await vaultsService.setActiveVault(props.vault.id)
                    router.push({ name: 'Vault', params: { id: props.vault.id } })

                } catch (error) {
                    Pop.toast(error, 'error')
                    logger.log(error)
                }
            }
        }
    }
}
</script>
    
    
    <style lang="scss" scoped>
    .img-tag {
        width: 250px;
        height: 250px;
        object-fit: auto;
    }
    
    .text-img {
        position: absolute;
        bottom: 2px;
        left: 5px;
        display: block;
        object-fit: contain;
        text-shadow: 2px 2px 4px black;
    }
    </style>
