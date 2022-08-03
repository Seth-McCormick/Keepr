<template>

    <div class=" mb-2 p-0 shadow rounded  selectable" @click="goToVaultPage">
        <div class=" ">
            <img class=" rounded img-tag" :src="vault.img" alt="">
        </div>
        <div class="text-img d-flex justify-content-between">
            <h4>{{ vault.name }}</h4>

        </div>
    </div>

</template >
    
    
    <script>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
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
    </style>
