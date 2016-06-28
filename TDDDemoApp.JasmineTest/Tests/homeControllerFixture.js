/// <reference path="_test_references.js" />

'use strict';

var testObject;
var scope;
var httpBackend;

describe("TDDDemoApp.JasmineTest.homeController", function () {
    beforeEach(module('TDDDemoApp'));

    beforeEach(inject(function ($controller, $rootScope, $httpBackend) {
        scope = $rootScope.$new();
        httpBackend = $httpBackend;
        testObject = $controller('homeController', { $scope: scope });
    }));

    it('sets default messageText to empty', function () {
        expect(testObject.messageText).toBe('');
    });

    it('sets messages to empty list', function () {
        expect(testObject.messages).toEqual([]);
    });

    it('initializes loaded to false', function () {
        expect(testObject.loaded).toEqual(false);
    });

    describe('init', function() {
        it('loads messages from url and marks loaded true', function () {
            var expectedMessages = [{ id: 1, message: 'foo' }];
            httpBackend.expect('GET', 'api/v1/messages').respond(200, expectedMessages);

            testObject.init();

            httpBackend.flush();

            expect(testObject.messages).toEqual(expectedMessages);
            expect(testObject.loaded).toBe(true);
        });

        it('does nothing on failure', function () {
            httpBackend.expect('GET', 'api/v1/messages').respond(500);

            testObject.init();

            httpBackend.flush();

            expect(testObject.messages).toEqual([]);
            expect(testObject.loaded).toBe(false);
        });
    });

    describe('addMessage', function() {
        it('adds new message to list', function () {
            var messageText = 'foo';
            testObject.messageText = messageText;
            testObject.messages = [{ id: 1, message: 'other' }];

            var expectedResult = { id: 4, message: messageText };

            httpBackend.expect('POST', 'api/v1/message', { message: messageText }).respond(200, expectedResult);

            testObject.addMessage();

            httpBackend.flush();

            expect(testObject.messages[0]).toEqual(expectedResult);
            expect(testObject.messageText).toEqual('');
        });
    });
});
