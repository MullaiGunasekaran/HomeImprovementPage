Feature: HomeImprovement
	

@ScreenShot
Scenario: Printing the Search Result
	Given I have launched the browser
	And  I have entered the WebsiteURL
	And I use datasheet "DataSheet.xml" and section "Search1"
	And I have entered the Search Text "datasheet:search"
	And I have entered the Postcode "datasheet:area"
	When I press Lookup Button
	Then the searchresults should be displayed
	And I click the Next Button
