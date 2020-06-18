var enableTour = false;

function enableTourMain() {
    enableTour = true;

    const tour = new Shepherd.Tour({
        defaultStepOptions: {
            cancelIcon: {
                enabled: true
            },
            classes: 'shadow-md bg-purple-dark',
            scrollTo: { behavior: 'smooth', block: 'center' }
        },
        useModalOverlay: true
    });

    tour.addStep({
        title: 'Bienvenue',
        text: `\n<p>\n Bienvenue dans votre interface d'archivage\n </p>\n
                \n<p>\n C'est parti pour le tour du propriétaire\n </p>\n`,
        buttons: [
            {
                action() {
                    return this.next();
                },
                text: 'Suivant'
            }
        ],
        id: 'creating'
    });

    tour.addStep({
        title: 'Barre de navigation',
        text: `\n<p>\n C'est depuis cette barre de navigation que vous pourrez vous rendre sur toutes les parties du site.\n </p>\n
                \n<p>\n \n </p>\n`,
        attachTo: {
            element: '.navbar-dark',
            on: 'bottom'
        },
        buttons: [
            {
                action() {
                    return this.back();
                },
                classes: 'shepherd-button-secondary',
                text: 'Retour'
            },
            {
                action() {
                    return this.next();
                },
                text: 'Suivant'
            }
        ],
        id: 'creating'
    });

    if ($("#connexion-block").length != 0) {
        tour.addStep({
            title: 'Espace de connexion',
            text: `\n<p>\n Ces deux boutons de connexion vous permettent de vous inscrire et de vous connecter.\n </p>\n
                \n<p>\n \n </p>\n`,
            attachTo: {
                element: '#connexion-block',
                on: 'bottom'
            },
            buttons: [
                {
                    action() {
                        return this.back();
                    },
                    classes: 'shepherd-button-secondary',
                    text: 'Retour'
                },
                {
                    action() {
                        return this.next();
                    },
                    text: 'Suivant'
                }
            ],
            id: 'creating'
        });

        tour.addStep({
            title: 'Inscription',
            text: `\n<p>\n Allez-y, cliquez sur le bouton d'inscription dès maintenant pour accéder à vos outils d'archivage.\n </p>\n
                \n<p>\n \n </p>\n`,
            attachTo: {
                element: '#inscrire-block',
                on: 'bottom'
            },
            id: 'creating'
        });
    }

    if (enableTour) {
        tour.start();
    }
}