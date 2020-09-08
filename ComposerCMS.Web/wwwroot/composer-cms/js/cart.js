class Cart {
    qtyIncreaseClass = 'composer-btn-qty-increase';
    qtyDecreaseClass = 'composer-btn-qty-decrease';

    constructor() {
        document.querySelectorAll(`.${this.qtyIncreaseClass}`).forEach((node) => {
            console.log(node);
            node.addEventListener('click', (event) => this.increaseItemQty(event.target.dataset.composerProductId));
        });

        document.querySelectorAll(`.${this.qtyDecreaseClass}`).forEach((node) => {
            node.addEventListener('click', (event) => this.decreaseItemQty(event.target.dataset.composerProductId));
        });
    }

    increaseItemQty = (productId) => {
        window.composer.fetch.post(`/api/cart/item/increase/${productId}`).then((data) => {
            console.log(data);
        });
    }

    decreaseItemQty = (productId) => {
        window.composer.fetch.post(`/api/cart/item/decrease/${productId}`).then((data) => {
            console.log(data);
        });
    }

    removeCartItem = (productId) => {

    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    window.composer = window.composer || {};
    window.composer.cart = window.composer.cart || new Cart();
});