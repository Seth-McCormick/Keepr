<template>

    <div class="col-md-12 shadow rounded  selectable" data-bs-target="#vault-keep-modal" data-bs-toggle="modal"
        @click="setActiveKeep">
        <div class=" ">
            <img class=" img-fluid background-image rounded" :src="vaultKeep.img" alt="">
        </div>
        <div class="text-img d-flex justify-content-between text-light">
            <h4>{{ vaultKeep.name }}</h4>
        </div>
        <div class="profile-img">

            <img class="action profile-img" @click="goToProfile" :src="vaultKeep.creator.picture" alt="">
        </div>
    </div>



</template>


<script>
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'

export default {
    props: {
        vaultKeep: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        const router = useRouter()
        return {
            goToProfile() {
                try {
                    router.push({ name: "Profile", params: { id: props.vaultKeep.creatorId } })
                } catch (error) {
                    Pop.toast("error")
                    logger.log(error)
                }
            },
            async setActiveKeep() {
                try {
                    keepsService.setActiveKeep(props.vaultKeep)
                } catch (error) {
                    Pop.toast(error.message, "error")
                    logger.log(error)
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.text-img {
    position: absolute;
    bottom: 2px;
    left: 5px;
    display: block;
    object-fit: contain;
    text-shadow: 2px 2px 4px black;
}

.profile-img {
    border-radius: 50%;
    height: 30px;
    position: absolute;
    bottom: 4px;
    right: 5px;
}
</style>