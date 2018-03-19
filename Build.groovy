pipeline {
	agent {
		label 'master'
	}

	stages {
		stage('Build') {
			steps {
				bat """
					nuget restore
					
					CALL bin\\msbuild_where.cmd \"Dapper.Scripts.sln\" /m ^
						/p:Configuration=Release ^
						/p:Platform=\"Any CPU\" ^
						/target:Build
				"""
			}
		}
		stage('Unit Test') {
			steps {
				bat "nunit3-console \"Dapper.Scripts.Tests\\bin\\Release\\Dapper.Scripts.Tests.dll\" --result=\"Dapper.Scripts.Tests\\bin\\Release\\TestResults.xml\""
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
