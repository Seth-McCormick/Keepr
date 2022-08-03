<template>

    <div class="col-md-11 mx-5 mt-5 d-flex justify-content-between">
        <h1>{{ vault.name }}</h1>
        <button class="rounded" @click="deleteVault">Delete Vault</button>
    </div>
    <div>
        <h5 class="ms-5">Keeps: {{ vaultKeeps.length }}</h5>
    </div>

    <div class="masonry-frame m-3">
        <div class="m-2" v-for="vk in vaultKeeps" :key="vk.id">
            <VaultKeep :vaultKeep="vk" />
        </div>
    </div>

</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    name: 'VaultPage',
    setup() {
        const router = useRouter()
        const route = useRoute()
        onMounted(async () => {
            await vaultsService.setActiveVault(route.params.id),
                await vaultsService.getVaultKeeps(route.params.id)

        })

        return {
            vault: computed(() => AppState.activeVault),
            vaultKeeps: computed(() => AppState.vaultKeeps),
            user: computed(() => AppState.user),
            account: computed(() => AppState.account),

            async deleteVault() {
                try {
                    if (await Pop.confirm()) {

                        await vaultsService.deleteVault(route.params.id)
                        await router.push({ name: 'Profile', params: { id: this.vault.creatorId } })
                    }
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
.masonry-frame {
    columns: 4;
    position: absolute;
}

div {
    break-inside: avoid;

}
</style>