pipeline {
	agent {
		label 'master'
	}

	stages {
		stage('Build') {
			steps {
				bat """
					nuget restore
					CALL bin\\msbuild_where.cmd \"Dapper.Scripts.sln\" /p:Configuration=Release /p:Platform=\"Any CPU\" /m
				"""

				//stash name: "unit-tests", includes: "Dapper.Scripts.Tests\\bin\\Release\\**"
			}
		}
		stage('Unit Test') {
			steps {
				//unstash "unit-tests"

				bat "nunit3-console \"Dapper.Scripts.Tests\\bin\\Release\\Dapper.Scripts.Tests.dll\""
			}
			post {
				always {
					archiveArtifacts artifacts: "Dapper.Scripts.Tests\\bin\\Release\\TestResults.xml"

					step([$class: 'NUnitPublisher',
						testResultsPattern: "Dapper.Scripts.Tests\\bin\\Release\\TestResults.xml",
						keepJUnitReports: true,
						skipJUnitArchiver: false,
						failIfNoResults: true,
						debug: false])
				}
			}
		}
	}
}
