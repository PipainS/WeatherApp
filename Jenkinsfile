pipeline {
    agent any

    environment {
        // Путь к папке с тестами
        TESTS_DIR = 'api-tests'
    }

    stages {
        stage('Checkout Backend') {
            steps {
                // Основной репозиторий (бэкенд) скачивается автоматически, 
                // если Jenkinsfile лежит в нем.
                echo 'Checking out Backend...'
            }
        }

        stage('Checkout Tests') {
            steps {
                // Скачиваем второй репозиторий (Java тесты) в подпапку
                dir("${env.TESTS_DIR}") {
                    checkout([$class: 'GitSCM', 
                        branches: [[name: 'master']], 
                        userRemoteConfigs: [[url: 'https://github.com/ilyentiy1/weatherTests']]
                    ])
                }
            }
        }

        stage('Build & Run Backend') {
            steps {
                echo 'Building .NET Backend...'
                sh 'dotnet build'
                // Здесь обычно запускается бэкенд в фоновом режиме или в Docker
                sh 'dotnet run &' 
            }
        }

        stage('Run Java API Tests') {
            steps {
                dir("${env.TESTS_DIR}") {
                    echo 'Running Java Tests...'
                    // Используем Maven или Gradle (JUnit 5 / TestNG)
                    sh './gradlew clean test' 
                }
            }
        }
    }

}