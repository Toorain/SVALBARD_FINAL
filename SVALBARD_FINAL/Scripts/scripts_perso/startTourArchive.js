// Doesn't call WebService if not on Datatable displaying page.*/
if (window.location.pathname === "/Pages/AfficherArchives") {
    var enableTour = false;

    function enableTourArchive() {
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
            text: `\n<p>\n Bienvenue sur le panneau de contrôle\n </p>\n
            \n<p>\n Découvrons ce qu'il est possible de faire dans cette rapide présentation \n </p>\n`,
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
            title: 'Nombre d\'entrées',
            text: `\n<p>\n Vous pouvez sélectionner le nombre de lignes que vous souhaitez afficher.\n </p>\n
            \n<p>\n \n </p>\n`,
            attachTo: {
                element: '#tableArchive_length',
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
            title: 'Barre de recherche',
            text: `\n<p>\n Cette barre de recherche vous permet de filtrer en direct les données qui vous intéressent\n </p>\n
        \n<p>\n \n </p>\n`,
            attachTo: {
                element: '#tableArchive_filter',
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
            title: 'Tri par colonne',
            text: `\n<p>\n Un simple clic sur une colonne vous permet de trier les données.\n </p>\n
        \n<p>\n \n </p>\n`,
            attachTo: {
                element: '#column-name',
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
            title: 'Recherche individuelle',
            text: `\n<p>\n Cette barre de recherche vous permet d'effectuer une recherche filtrée sur chaque colonne\n </p>\n
        \n<p>\n \n </p>\n`,
            attachTo: {
                element: '#column-search',
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
            title: 'Actions sur une archive',
            text: `\n<p>\n En cliquant sur une ligne vous pouvez faire apparaître les actions possibles sur cette archive.\n </p>\n
        \n<p>\n \n </p>\n`,
            attachTo: {
                element: 'tbody',
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
            title: 'Pagination ',
            text: `\n<p>\n Permet la navigation entre les différentes pages\n </p>\n
                    \n<p>\n PRO TIP : Il est possible de naviguer avec les flèches directionnelles.\n </p>\n`,
            attachTo: {
                element: '#tableArchive_paginate',
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
                        return this.complete();
                    },
                    text: 'Fin'
                }
            ],
            id: 'creating'
        });

        if (enableTour) {
            tour.start();
        }
    }
}