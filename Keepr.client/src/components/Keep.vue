<template>
    <div class="col-md-3 shadow rounded background-image img-fluid selectable" @click="setActiveKeep"
        :style="{ 'background-image': `url(${keep.img})` }">
        <div class="d-flex justify-content-between">
            <h4>{{ keep.name }}</h4>
            <img class="action" @click="goToProfile" :src="keep.creator.picture" alt="">
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
        keep: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        const router = useRouter()

        return {

            goToProfile() {
                try {
                    router.push({ name: "Profile", params: { id: props.keep.creatorId } })
                } catch (error) {
                    Pop.toast("error")
                    logger.log(error)
                }
            },

            async setActiveKeep() {
                try {
                    keepsService.setActiveKeep(props.keep.id)
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
.background-image {
    object-fit: contain;
}

img {
    border-radius: 50%;
    height: 30px;
}
</style>