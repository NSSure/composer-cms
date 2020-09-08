class Fetch {
    async post(url = '', data = {}) {
        const response = await fetch(url, {
            method: 'POST',
            mode: 'cors',
            cache: 'no-cache',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        return response.json();
    }
}

window.composer = window.composer || {};
window.composer.fetch = window.composer.fetch || new Fetch();